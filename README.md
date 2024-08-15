# Tower Defense Game Project
This Unity-based project demonstrates our skills in game development, software architecture, and C# programming. Below are the key features and systems implemented in this project. Various architectural refactors were made during the lifetime of this project due to our learnings while building it. The architecture outlined in the README is the most recent state of this project and can be found under branch architecture-refactor-2, however, some non-architecture level features may be found in other branches.

# Table of Contents

1. [Key Features](#key-features)
   1. [Enemy System](#1-enemy-system)
   2. [Wave Management](#2-wave-management)
   3. [Tower/Unit System](#3-towerunit-system)
   4. [Combat System](#4-combat-system)
   5. [Resource Management](#5-resource-management)
   6. [Unit Placement and UI Systems](#6-unit-placement-and-ui-systems)
   7. [Upgrade System](#7-upgrade-system)
   8. [Advanced Projectile System](#8-advanced-projectile-system)
   9. [State Machine Architecture](#9-state-machine-architecture)
   10. [Event-Driven Architecture](#10-event-driven-architecture)
   11. [Singleton Pattern](#11-singleton-pattern)

2. [Technical Highlights](#technical-highlights)

3. [Development Branches](#development-branches)

4. [Architectural Implementation Details](#architectural-implementation-details)

   1. [State Machine Pattern Implementation](#state-machine-pattern-implementation)
      - [Implementation Overview](#implementation-overview)
   
   2. [Event-Driven Architecture](#event-driven-architecture)
      - [Implementation Overview](#implementation-overview-1)
   
   3. [Singleton Pattern Implementation](#singleton-pattern-implementation)
      - [Implementation Overview](#implementation-overview-2)

## Key Features
### 1. Enemy System

* Enemies spawn in waves and follow predefined paths
* Different enemy types with unique attributes (health, speed, etc.)
* Enemy state machine for managing behavior (moving, attacking)

### 2. Wave Management

* Multiple waves of enemies with increasing difficulty
* Wave data defined using ScriptableObjects for easy configuration
* Start wave button to give players control over wave timing

[![Image from Gyazo](https://i.gyazo.com/6c0bd97ef77ee90792b8ac4b58256a44.gif)](https://gyazo.com/6c0bd97ef77ee90792b8ac4b58256a44)

### 3. Tower/Unit System

* Players can place defensive units on the map
* Units have different attributes (attack damage, range, attack speed)
* Units automatically target and attack nearby enemies

### 4. Combat System

* Real-time combat between units and enemies
* Damage calculation and health management
* Visual feedback for attacks and damage taken

[![Image from Gyazo](https://i.gyazo.com/52044fc76b63f2854bf776c163761891.gif)](https://gyazo.com/52044fc76b63f2854bf776c163761891)

### 5. Resource Management

* Gold system for purchasing and upgrading units
* Lives system to track player health

### 6. Unit Placement and UI Systems

* Intuitive drag-and-drop interface for unit placement
* Dynamic range indicators for precise unit positioning
* Healthbars for enemies
* Unit selection hotbar for quick access to different unit types
* Upgrade buttons integrated into unit UI
* Terrain layer system to determine placable and non-placable locations

### 7. Upgrade System

* Three distinct upgrade paths for each unit type
* Multiple upgrades available within each path
* Upgrade effects dynamically applied to units
* Modular to ensure easier extendibility
  
[![Image from Gyazo](https://i.gyazo.com/9ed04dffabc9a26a9bd9b51b3df44e2f.gif)](https://gyazo.com/9ed04dffabc9a26a9bd9b51b3df44e2f)

### 8. Advanced Projectile System
* Diverse array of projectile behaviors (chaining, AOE, splitting, etc.) adding strategic depth to combat
* Projectile effects implemented through a flexible, modular design
* Projectile behavior is customizable through a scriptable object system
[![Image from Gyazo](https://i.gyazo.com/a8dd90bc1e2eed9ead8bd2e3b0b2f82e.gif)](https://gyazo.com/a8dd90bc1e2eed9ead8bd2e3b0b2f82e)

### 9. State Machine Architecture

* Robust state machine for managing game states (preparation, wave in progress, game over)
* Enemy and unit behavior controlled by individual state machines

### 10. Event-Driven Architecture

* Utilizes an event bus system for decoupled communication between game components
* Improves modularity and maintainability of the codebase

### 11. Singleton Pattern

* Efficient management of global game systems and managers

### Technical Highlights

* C# Programming: Demonstrates proficiency in C# and object-oriented programming principles
* Unity Engine: Leverages Unity's features for game development
* Design Patterns: Implements Singleton, State, and Factory patterns
* ScriptableObjects: Uses ScriptableObjects for data management and configuration
* Code Organization: Well-structured codebase with clear separation of concerns

### Development Branches
This project uses a branching strategy to manage feature development. Some features mentioned above may only be available on specific branches as they are still in development. 

# Architectural Implementation Details
## State Machine Pattern Implementation

This project extensively uses the State Machine pattern to manage game flow, enemy behavior, and unit actions. This design choice offers several benefits:

- **Modularity**: Each state encapsulates its own logic, making it easy to add or modify behaviors.
- **Clarity**: The current state of any entity (game, enemy, or unit) is always explicit.
- **Flexibility**: Transitions between states are clearly defined and easily manageable.

### Implementation Overview

The pattern is implemented across three main areas:

1. **Game States**: Manage overall game flow (e.g., Preparation, WaveInProgress)
2. **Enemy States**: Control individual enemy behavior (e.g., Moving, Combat)
3. **Unit States**: Handle player unit actions (e.g., Idle, Combat)

Each area follows a similar structure:

```csharp
// State interface
public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

// State manager (e.g., GameManager, EnemyController, UnitController)
public class StateManager
{
    private IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        currentState?.Update();
    }
}

// Example concrete state
public class ConcreteState : IState
{
    public void Enter() { /* Initialize state */ }
    public void Update() { /* State-specific behavior */ }
    public void Exit() { /* Cleanup */ }
}
```

This pattern allows for complex behaviors to be broken down into manageable, independent states. It facilitates easy addition of new states and modifications to existing ones, contributing to our project's scalability and maintainability.

This pattern was implemented as part of an architecture refactor (branch architecture-refactor-2) because of the shortcomings of the initial approach. This approach makes things much easier to maintain and scale.

This pattern allows for easy expansion of behavior by adding new states, and it keeps the logic for each state encapsulated and separate from other states. It also makes it easier to handle complex behaviors and transitions in our code.

## Event-Driven Architecture

This project implements a robust event-driven architecture using an EventBus system. This design choice provides several advantages:

- **Decoupling**: Components communicate without direct dependencies, enhancing modularity.
- **Scalability**: Easy to add new events and subscribers without modifying existing code.
- **Flexibility**: Allows for complex interactions between different parts of the game.

### Implementation Overview

The event system is centered around the `EventBus` class, which acts as a central hub for publishing and subscribing to events.

```csharp
public class EventBus : Singleton<EventBus>
{
    private Dictionary<Type, Delegate> eventHandlers = new Dictionary<Type, Delegate>();

    public void Subscribe<T>(Action<T> handler) { /* ... */ }
    public void Unsubscribe<T>(Action<T> handler) { /* ... */ }
    public void Publish<T>(T eventData) { /* ... */ }
}
```

Events are simple C# classes:

```csharp
public class EnemyDefeatedEvent
{
    public Enemy DefeatedEnemy { get; }
    public EnemyDefeatedEvent(Enemy enemy) => DefeatedEnemy = enemy;
}
```

Components can subscribe to and publish events:

```csharp
// Subscribing
EventBus.Instance.Subscribe<EnemyDefeatedEvent>(OnEnemyDefeated);

// Publishing
EventBus.Instance.Publish(new EnemyDefeatedEvent(defeatedEnemy));

// Event handler
private void OnEnemyDefeated(EnemyDefeatedEvent evt)
{
    // Handle the event...
}
```

This system is used extensively throughout the project for various purposes:

1. **Game State Management**: Notifying components about game state changes.
2. **Combat System**: Handling attacks, damage, and defeats.
3. **UI Updates**: Keeping the user interface in sync with game events.
4. **Wave Management**: Coordinating enemy spawning and wave progression.

The event-driven architecture allows for a highly modular and extensible codebase. It facilitates easy addition of new features and modification of existing ones, as components can react to events without needing to know about the source.

This pattern was implemented as part of an architecture refactor (branch architecture-refactor-2) because of the shortcomings of the initial approach. This implementation demonstrates a strong understanding of software architecture principles and a commitment to creating maintainable, scalable code.

## Singleton Pattern Implementation

This project utilizes the Singleton pattern for managing global access to crucial game systems. This design choice offers several advantages:

- **Global Access**: Provides a single point of access to important managers and systems.
- **State Preservation**: Ensures only one instance exists, maintaining consistent state.
- **Resource Efficiency**: Prevents multiple instantiations of resource-heavy systems.

### Implementation Overview

We've implemented a generic `Singleton<T>` class that other classes can inherit from to become singletons:

```csharp
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString() + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
}
```

This pattern is used for several key systems in the project:

1. **GameManager**: Oversees the overall game state and flow.
2. **EventBus**: Manages the event-driven communication system.
3. **ResourceManager**: Handles in-game resources and economy.
4. **WaveManager**: Controls enemy wave spawning and progression.
5. **EnemyManager**: Manages enemy entities in the game.

Usage example:

```csharp
public class GameManager : Singleton<GameManager>
{
    // GameManager implementation...
}

// Accessing the GameManager anywhere in the code
GameManager.Instance.SomeMethod();
```

The Singleton pattern, combined with Unity's `MonoBehaviour`, allows these systems to persist across scenes and provides easy, global access throughout the codebase. This approach facilitates centralized control over key game systems while maintaining clean and modular code.

While the Singleton pattern is sometimes controversial due to its global state, we've carefully chosen its application to systems that genuinely require a single, globally accessible instance. This demonstrates a thoughtful approach to architecture, balancing convenience with best practices in software design.
