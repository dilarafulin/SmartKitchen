# RecipeRush

**A cooking game with a Reinforcement Learning powered AI assistant, built in Unity with ML-Agents.**

RecipeRush is an Overcooked-style, top-down cooking game featuring an autonomous AI agent called the **Sous Chef**. Instead of being scripted with hand-written rules, the Sous Chef is trained with **Reinforcement Learning (PPO)** using Unity ML-Agents. It learns to navigate the kitchen and complete cooking tasks — fetching, chopping, cooking, and delivering — entirely on its own, without being given explicit direction vectors toward its targets.



## Features

- **RL-trained AI assistant** — the Sous Chef learns kitchen navigation and task execution through trial and error, not scripted behavior.
- **Single multi-task policy** — one neural network handles four different task types (Fetch, Chop, Cook, Deliver) based on the active command.
- **Dual perception** — a 13-value vector observation (target position, distance, interaction range, held item, command one-hot) combined with a **Ray Perception Sensor 3D** for obstacle detection.
- **Data-driven design** — all ingredients, recipes, and transformation rules are defined as Unity **ScriptableObjects**, editable without touching code.
- **Task chaining** — atomic tasks are composed into full recipes (e.g. "chop the tomato and plate it") through chain structures.
- **Per-task training metrics** — custom `StatsRecorder` logging exposes success rates for each task type in TensorBoard.

---

## Tech Stack

| Area | Technology |
|------|-----------|
| Game Engine | Unity 6 (6000.3.10f1) |
| Rendering | Universal Render Pipeline (URP) |
| Language | C# |
| AI / RL | Unity ML-Agents Toolkit 4.0.2 |
| Algorithm | PPO (Proximal Policy Optimization) |
| Training Backend | Python + PyTorch |
| Model Runtime | ONNX (in-engine inference) |
| Input / Camera | Input System, Cinemachine |
| Monitoring | TensorBoard |

---

## How the Agent Works

The Sous Chef agent (`SousChefAgent.cs`) extends the ML-Agents `Agent` class.

**Observations (13 vector values + ray sensor):**
- Relative position to target (x, z)
- Target position (x, z)
- Distance to target
- Whether in interaction range (0/1)
- Whether holding an ingredient (0/1)
- Whether the target counter is occupied (0/1)
- Active command as a one-hot vector (Fetch, Chop, Cook, Deliver, Idle)

All positions are normalized. The agent is deliberately **not** given a ready-made direction vector — it learns the mapping from raw relative coordinates to movement direction itself.

**Actions (2 discrete branches):**
- Movement: idle / forward / back / left / right
- Interaction: interact / don't interact

**Reward shaping:**
- `+1` for completing a task
- **Potential-based shaping** for approaching the target (symmetric, so reward farming is impossible)
- Small step penalty to encourage efficiency
- Penalties for going out of bounds, timing out, or burning food
- Intermediate rewards for multi-step tasks (chopping, cooking)

---

## Project Structure

```
RecipeRush/
├── Assets/
│   └── Scripts/
│       ├── SousChef/            # RL agent + task management
│       │   ├── SousChefAgent.cs         # The RL agent (observations, actions, rewards)
│       │   ├── SousChefTaskManager.cs    # Assigns and tracks tasks
│       │   ├── SousChefTrainingManager.cs# Generates tasks during training
│       │   ├── SousChefCommand.cs        # Command enum (Fetch/Chop/Cook/Deliver/Idle)
│       │   └── *Chain.cs                  # Multi-step task chains
│       ├── Counters/            # Interaction stations (BaseCounter hierarchy)
│       ├── ScriptableObjects/   # Ingredient & recipe dataset (43 assets)
│       ├── Player.cs            # Human player controller
│       └── ...
├── config/
│   ├── souschef.yaml            # ML-Agents training configuration
│   └── convert_to_onnx.py       # Checkpoint → ONNX conversion script
└── results/                     # Training runs (souschef_v1 ... v26+)
```

---

## Getting Started

### Requirements
- Unity 6 (6000.3.10f1 or compatible)
- Python 3.10+ with a virtual environment
- Unity ML-Agents (`pip install mlagents`)

### Running the Game
1. Open the project in Unity.
2. Open the main kitchen scene and press **Play**.
3. The Sous Chef runs the trained ONNX model automatically — no Python needed.

### Training the Agent
1. Activate your Python virtual environment.
2. Start training:
   ```bash
   mlagents-learn config/souschef.yaml --run-id=souschef_vX
   ```
3. Press **Play** in Unity when prompted to connect the environment.
4. Monitor progress:
   ```bash
   tensorboard --logdir results
   ```

### Exporting a Model to ONNX
```bash
python config/convert_to_onnx.py
```

---

## Training Configuration

Key hyperparameters (`config/souschef.yaml`):

| Parameter | Value |
|-----------|-------|
| Trainer | PPO |
| Max steps | 1,000,000 |
| Learning rate | 3.0e-4 |
| Batch size | 64 |
| Buffer size | 2048 |
| Hidden layers | 2 × 128 units |
| Gamma | 0.99 |
| Normalize | true |

---

## License

This project was developed for academic purposes as a graduation project.
