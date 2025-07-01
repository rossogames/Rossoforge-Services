# Rosso Games

<table>
  <tr>
    <td><img src="https://github.com/rossogames/Rossoforge-Services/blob/master/logo.png?raw=true" alt="Rossoforge" width="64"/></td>
    <td><h2>Rossoforge-Service</h2></td>
  </tr>
</table>

**Rossoforge-Service** is a lightweight and thread-safe Service Locator designed for Unity.  
It provides a centralized way to register, retrieve, and manage services at runtime without relying on static singletons.

**Version**: Unity 6 or higher

**Tutorial**: https://youtu.be/iMHBhLSjnYc

**Dependencies**
* [Rossoforge-Core](https://github.com/rossogames/Rossoforge-Core.git)


#
With this package, you can:

- Register and retrieve services by interface using a clean API.
- Automatically initialize services that implement `IInitializable`.
- Dispose of services gracefully when unregistered.
- Use a thread-safe `DefaultServiceLocator` implementation.

#

```csharp
// Setup
var locator = new DefaultServiceLocator();
locator.Register<IMyService>(new MyService());
locator.Initialize();
ServiceLocator.SetLocator(locator);

// Anywhere in your code
var myService = ServiceLocator.Get<IMyService>();
```

#
This package is part of the **Rossoforge** suite, designed to streamline and enhance Unity development workflows.

Developed by Agustin Rosso
https://www.linkedin.com/in/rossoagustin/
