# Signals - Signals library for Unity

A simple straight forward signals library for Unity. Send a signal from anywhere in your project and let other classes freely capture and respond to the event.

# Table of Contents

- [Installation](#installation)
- [Quick Start](#quick-start)
- [Contributing](#contributing)

# Installation

Currently the best way to include this package in your project is through the unity package manager. Add the package using the git URL of this repo: https://github.com/justinleemans/signals

# Quick Start

To start using this library first you have to make an instance of the manager. This instance will be your central system through which to route all your signals.

```c#
SignalManager manager = new SignalManager();
```

Note: you want to keep this instance as a single instance. Maybe keeping track of this instance on a central systems singleton or through dependency injection, since using different instances of this manager will mean that subscribtions and sending will be divided over these instances.

## Sending

Tos send a signal you can either get a signal and populate any fields you have on your signal class by getting a signal with `GetSignal<TSignal>()` and sending it with `Send(signal);`.

```c#
var signal = manager.GetSignal<ExampleSignal>();
signal.Foo = "bar";
manager.Send(signal);
```

Or send it directly with `Send<TSignal>()` without populating fields.

```c#
manager.Send<ExampleSignal>();
```

## Subscribing

You can subscribe to a signal using the manager and calling `Subscribe<TSignal>(OnSignal)` where the handler is a delegate with a signal instance of the given signal type as parameter.

```c#
manager.Subscribe<ExampleSignal>(OnExampleSignal);
...
private void OnExampleSignal(ExampleSignal signal)
{
}
```

To unsubscribe from a signal you make a call similar to subscribing by calling `Unsubscribe<TSignal>(OnSignal)` with the same method as you used to subscribe earlier.

```c#
manager.Unsubscribe<ExampleSignal>(OnExampleSignal);
```

## Creating signals

All signals need to inherit from the `ISignal` interface. This interface doesn't have any added funtionality yet but this could change later on.

```c#
public class ExampleSignal : ISignal
{
}
```

One nice thing about this is that you can make other interfaces from this to extend functionality per signal. For example you could make an interface called `IAudioSignal` which itself inherits from `ISignal` which holds a string which references an audio clip. Then in your audio manager you can subscribe to `IAudioSignal` and get the identifier to play this sound. This way you don't have to subscribe to every signal with audio individually and attatching audio to a signal just became a whole lot easier.

# Contributing

Currently I have no set way for people to contribute to this project. If you have any suggestions regarding improving on this project you can make a ticket on the GitHub repository or contact me directly.
