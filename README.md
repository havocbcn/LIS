# LIS

![UML](/images/uml.png?raw=true)

It starts with an `Order` that adds new `Tests` with a name (string) using a `TestFactory` class, that uses reflection to instantiate the `Test`.
The `TestFactory` looks up for all `Tests` in the assembly that are marked using a `TestAttribute` attribute with a Name.

-   Using a `TestFactory` with reflection allows it to add new tests class and do not touch anything else, like plugins that extend functionality.
-   Using attributes instead of a property that must be overridden allows the `TestFactory` to have a dictionary with name and Tests without instantiating anything, only when `CreateTest` in the `TestFactory` is executed then a new instance of the test is created.
-   There are 2 tests to look up for `Test` without `TestAttribute` or multiple `Tests` with the same name.
    Note: using a more conservative way, it could be done using an abstract Name property that must be overridden in `Test` instances, but imply that `TestFactory` must instantiate all tests (with a new parameterless constructor) only to discover the `Test` names. Developers must implement a name to compile but repeated `Test` name errors are not discovered in compilation time, so a test must be placed as well. The attribute solution in my opinion is more elegant.

`Test` is an abstract class that has some common properties, each test type (`BiochemistryTest`, `ImmunologyTest`, `MicrobiologyTest`, `HematologyTest`) are abstract classes as well derived from `Test`. Each real test will be a class implementing one of the test types.
The `Test` declares an abstract `SetCalculation` method that all instances must implement. This decision is not in the documentation but I understood that `Operations` set a result that an actual `Test` must interpret, so `Operations` call the `SetCalculation` to give the result and the `Test` set it's inner properties in consequence.

To a `Test` it can be added `Operations` in the same way, using the operation name that an `OperationFactory` will look up in the assembly to add a new instance. The `Operation` has an abstract `Execute` method that receives a generic `Test`, an implementation of `Operation` will implement an `Execute` that decides if a common operation is done to the `Test` or maybe cast the `Test` to an actual test type -like `BiochemistryTest`- to do a more specialized operation.
I do not split `Operation` classes in something like `GenericOperation`, `BiochemistryOperation`, `ImmunologyOperation`... because:

-   a developer could create a specific operation and forget to create the generic one causing that adding an operation to a test then no operation is found.
-   An operation code will be spread in some different classes, it will introduce some complexity.
-   Introduces a Parallel Inheritance Hierarchies code smell. 
-   I don't have more information, if the operation code is big or the same operation for a Biochemistry or Immunology doesn't have anything in common then a hierarchy of `Operation`  mimicking the test type class will be ok.

When the `Order` is fully created it can be `Executed`, that calls the `Execute` in all tests that call the `Execute` in all operations that invoke the `SetCalculation` in the `Test` that sets the inner properties of the tests.

To maintain the solution simple, I didn't include the test module that would manage all `Orders`, neither the Patient's sample that is related to the `Tests`. 

## Extensibility

-   New tests and operations are just  new classes. No more changes are needed. Reflection will do instantiating and unit testing will ensure correctness.

## Maintainability

-   Each operation code has all code and are defined independently.
-   Each particular tests has the code to interpret the results (`SetCalculation`).- Test type hierarchy has only properties, maybe it will expand if test types evolve.

## Design patterns

-   *Bridge*: instead of having tests with the operations code (properties and behaviours all together), they are two different classes:  `Test` uses `Operations` with an abstraction that have all the operations allowed. I know that the pattern does not fit perfectly because test call operations that call tests again, but the main idea remains, each class is separated to not bloat the class.
-   *Factory:* to allow creating new elements (and with reflection to do not tight the factory with the instances)
-   *Plugin structure*, it is not a design pattern, but guides the general structure: an interface -a abstract class in this case- that must be implemented to extend the functionality.   
-   *SOLID*

### Other techniques used

-   *TDD* (started with inside out and changed to outside in, the github history will show)
-   *Semantic commit messages*
-   *Object callisthenics*

### Proposed project structure ###
Using *DDD*, the domain is quite small (one bounding context) so I only used to guide the folder structure: Domain, Domain services and Exceptions. I do not include Repositories or Proxies.

Class names are took from the documentation.

## Refactoring strategy

*Disclaimer*: I completely invented the current procedural code, no refactor strategy could be done without seeing the proper code.

### Inject dependencies to allow testing

In some conditions, no static or "new" code, or third party code (like saving to a database) must be executed, testing cannot be done with it

This code will be mocked in tests so the idea is move the code to a new class with an interface, the class will be injected to the code, and the code will use the interface.

In some cases, it is necessary to create seams for code that cannot be moved.

### Create tests

Some tests must be placed (characterization test or golden master, depending on the code) using mocks to provide or collect information or disallowing saving data or sending emails.
Use any code coverage tool to look up relevant code not tested: SonarQube, OpenCover, Coverlet, dotCover... and repeat until a good safety net is placed.

### Refactor

Note: each operation will be a small one, no new functionality are introduced.

The code will mimic the new structure using only one test type (with all test code) and one operation (with all operations code), after that, some specialized tests or operations will be created moving code from the generic code.

-   Join operations code as much as possible
-   Join test properties
-   Join SetCalculation code (the part of operations that set test parameters)- Move test and SetCalculation to a new generic Test class (move code to a new class): it  will have all tests properties with all SetCalculation code,  the old code will create instances of this test, run the operations and set the result to it.
-   Inject the Test Factory- Call to test factory to create the test, if no test could be created use the previous generic test class- Create specific tests, one at a time, moving  the code from the generic test to specialized tests- Inject the Operation Factory
-   Call the operation factory, if no code is returned then old code is executed
-   Move operations to new operation's classes, one at a time, same strategy with tests: inject the test factory, call it, if no test is found use the old code, move test code to the new classes

