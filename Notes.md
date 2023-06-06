# Additional Notes

## Table of Contents
- [Exception and Error Handling](#exception-and-error-handling)
- [Tests: ApiController and ErrorHandlerMiddleware](#tests-apicontroller-and-errorhandlermiddleware)
- [Validation](#validation)
- [Logging](#logging)
- [DateTimeProvider](#datetimeprovider)
- [Authentication](#authentication)
- [Email Service using SendGrid](#email-service-using-sendgrid)
- [File Storage with Amazon S3](#file-storage-with-amazon-s3)
- [Caching with Distributed Memory Cache](#caching-with-distributed-memory-cache)
- [Outbox Pattern](#outbox-pattern-with-entity-framework)

## Exception and Error Handling
For flow control in this project, I decided to use a combination of **exception throwing and Result pattern**. I realized that throwing exceptions can be expensive, especially in cases where errors are expected or frequent. By using the Result pattern, we can make a clearer distinction between **expected and unexpected behaviors**, and we can return a result indicating success or failure without the **performance** penalty associated with exceptions.

Having errors defined in a one **centralized location**, makes it easier to maintain and understand all the expected error scenarios. By using middleware we are creating centralized exception handling in the application. This makes it easy to maintain and modify the code when needed. The ExceptionHandlerMiddleware captures any unhandled exceptions and logs them. It then maps the exception to an appropriate HTTP status code and returns **consistent error response** in the form of a ProblemDetails object.    
The ApiController class provides a base controller with a HandleResult method that processes the result of a domain operation and returns an appropriate action result based on the outcome. This method checks for specific error codes and maps them to the corresponding HTTP status codes, ensuring that the client receives meaningful feedback.

By using ProblemDetails class and the ErrorDetailsFactory I want to ensure that error and exception responses are consistent across the application. This makes it easier for client to consume and handle error responses. Media type for representing error responses I used is **application/problem+json** which is standardized media type defined by IETF in RFC 7807, designed to provide more structured and uniform and machine-readable way of expressing problem details in HTTP APIs.   

As a next step, I would consider improving the HandleResult method to reduce the use of conditional and switch statements, making it more streamlined and easier to maintain.  

## Tests: ApiController and ErrorHandlerMiddleware
To ensure that the error handling strategy is functioning as expected, I created several unit tests for the ApiController and ErrorHandlerMiddleware classes. I utilized the xUnit framework and FluentAssertions library to write these tests, aiming to enhance the expressiveness and readability of the test cases.

In the ApiControllerTests I have tested various scenarios, such as a successful operation, a failure with a custom error, validation errors, and authentication errors. These tests ensure that the HandleResult method returns the correct HTTP status codes and ProblemDetails objects for each case. I have additionally created TestApiController class which is subclass of ApiController class and is used to expose the HandleResult method which is protected in the ApiController class. This allows me to test the method in isolation without the need for complex setup or additional dependencies.

The ErrorHandlerMiddleware tests focus on testing the middleware's behavior when different exceptions are thrown. I have tested scenarios involving generic exceptions, custom AppExceptions, and UnauthenticatedExceptions. These tests confirm that the middleware correctly captures the exceptions and returns appropriate HTTP status codes along with ProblemDetails objects. The ExceptionHandlerMiddlewareFixture is a test fixture that sets up a test environment for the ExceptionHandlerMiddleware. It manages a TestServer and HttpClient, making it easy to simulate requests and analyze HTTP responses in test cases. This simplifies testing the middleware and keeps the test cases focused.   

These tests also serve as a foundation for future improvements and refinements to the error handling approach, as they will help identify any potential issues that may arise from changes in the code.  

## Validation
**Application-level validation**  
For implementing validation on application level I have used FluentValdation library and MediatR. The ValidationPipelineBehavior class ensures that request validation is performed before executing the request handler. It provides early feedback to users by rejecting invalid input as soon as possible. This helps catch validation errors early and prevents invalid data from propagating through the application.  

**Domain-level validation**  
This type of validation is based on the specific rules and invariants that must be maintained within your domain model. It enforces the correctness of the domain model and ensures that any business logic operation performed on entities are valid.

By having validation at both the Application and Domain levels, we can:  
1. provide early feedback to users by rejecting invalid input as soon as possible (Application-level validation). 
2. Protect the integrity and consistency of your domain model by enforcing business rules and invariants (Domain-level validation). 
3. Create a more robust and resilient system by having multiple layers of validation, preventing invalid operations from reaching critical parts of your application.

## Logging
Effective logging helps developers monitor, debug, and diagnose issues efficiently. Serilog is a popular and powerful logging library for .NET applications, that supports a variety of sinks, enabling log data to be written to multiple destinations such as files, consoles, and databases. Serilog can be easily configured through the appsettings.json file and can be modified without changing the application's code. The UseSerilog method in Program.cs instructs the application to use Serilog for logging and it reads the configuration from the appsettings.json file.  
The LoggingPipelineBehavior is implemented as an application layer middleware. This pipeline behavior is generic, so it can be applied to any IRequest and Result combination. It logs information about the request and response (request name, execution time, and errors if encountered during processing). 

## DateTimeProvider
The purpose of a DateTimeProvider is to provide a centralized way of obtaining the current date and time that is consistent throughout the application. It can help simplify time-related operations, improve consistency throughout the application, and make unit testing easier. If you use DateTime.UtcNow in multiple places and later you decide to change how you obtain the current date and time, you would need to modify each of those places.

## Authentication
**Setting Up the Identity Framework**  
To implement authentication system in the project, I utilized the Identity framework. I created AddIdentity and AddAuth extension methods for the IServiceCollection, which set up the IdentityDataContext, registered necessary services like UserManager, SignInManager, and RoleManager, and configured various identity options such as password requirements and unique email enforcement.

**User Entity**  
I placed the User entity in the Infrastructure layer because it's tightly coupled with the Identity Framework, which is an implementation detail. This decision allows us to keep the Domain layer clean and focused on the core business logic. However, this choice also means that we need to work with DTOs when interacting with the Application layer, as we cannot directly access the User entity from there.  

**IAuthService Interface and Implementation**  
To handle user authentication-related operations, I created a separate IAuthService interface in the Application layer. This interface contains methods for user registration, login, and retrieval of the current user. The AuthService class, which implements this interface, utilizes the UserManager and SignInManager to perform the desired operations.

## Email Service using SendGrid
I implemented an email sending feature using SendGrid as the email service provider. To achieve this, I created two separate classes: EmailSender and EmailService. The EmailSender is responsible for constructing the content of the email, such as generating confirmation links and reset password links, while the EmailService handles the actual sending of the email using the SendGrid API.

By using options pattern to manage the SendGrid settings, I was able to configure the API key and other necessary settings in the appsettings.json file. This approach provides flexibility (switching between different email service providers) and maintainability (settings can be changed by updating configuration file and without modifying the code directly) and security (sensitive information, like API keys are not hardcoded in the source code).

I ensured separation of concerns in my email implementation by dividing responsibilities between the EmailSender and EmailService classes. The EmailSender focuses on constructing the content of the email, while the EmailService handles the actual sending of the email using the SendGrid API.  
I included logging within the EmailService class to track the success or failure of email sending operations. Error handling is implemented with custom error classes to handle email sending failures, providing a consistent and clear way of reporting errors within the application.

To improve this implementation I could consider adding more errors for specific cases, such as handling temporary email sending failures or rate limiting issues. Additionally, I could look into implementing a more sophisticated retry mechanism for transient failures or further optimizing the performance by introducing a message queue or background processing.

## File Storage with Amazon S3
Amazon S3 integration has been implemented in the application to handle file uploads and deletions. The S3Service class uses the AmazonS3Client from the AWSSDK.S3 library to interact with the S3 service. The options pattern was used to define an S3Settings class, which holds the necessary settings for AmazonS3Client configuration (access key, secret key, and bucket name). These settings are read from the secrets and appsettings.json file and injected into the S3Service class.  
S3Service contains methods for uploading and deleting files from the bucket.

I have created a custom exception called S3FileDeletionException. In case an AmazonS3Exception occurs during the file deletion process, I log the original exception and rethrow the custom one. This allows me to have better control over the error handling process and decide which information I want to send to the client.

In the future, I could improve this implementation by implementing retries for transient failures and enhancing error handling with more specific custom exceptions.

## Caching with Distributed Memory Cache
I have implemented caching in my application using the .NET Core's built-in distributed memory cache. The CacheService class uses the IDistributedCache interface to interact with the cache provider, making it easy to switch to other providers like Redis in the future. I used the dependency injection pattern to register the ICacheService interface and its CacheService implementation, allowing for easy management of their lifetimes and injecting them into other parts of the application.

CacheService provides methods for getting, setting, and removing cache entries. It also implements the cache-aside pattern by offering a Get method with a factory function to handle cache misses efficiently. I have implemented a RemoveByPrefix method to remove multiple cache entries based on a key prefix, utilizing a ConcurrentDictionary to store cache keys and manage their removal.

In the future, I could improve this implementation by integrating a more scalable cache provider like Redis and implementing cache eviction policies to better manage the cache's memory usage.

## Outbox Pattern
The Outbox Pattern is an approach to handle message processing in distributed systems while maintaining transactional consistency. It involves storing messages, usually Domain Events, in an "Outbox" table in the database before publishing them to a message broker or another service. The Outbox pattern can prevent message loss and ensure that messages are eventually delivered to their intended recipients, even in the face of failures or network issues.   

The ConvertDomainEventsToOutboxMessagesInterceptor class extends the SaveChangesInterceptor and it intercepts and converts domain events to outbox messages. This interceptor captures the domain events generated by aggregate roots and converts them into outbox messages before they are saved to the database.  

I have created the ProcessOutboxMessagesJob class, which is a Quartz job responsible for fetching and processing outbox messages. The job queries the database for unprocessed messages and processes them in batches. To improve the reliability of the job, a retry policy using the Polly library has been implemented, which automatically attempts to process the messages again in the event of an exception.

For each outbox message, domain event is deserialized and the IPublisher interface is used to publish the event. If the event cannot be deserialized, the issue is logged for debugging purposes. Once an outbox message is processed successfully, ProcessedOnUtc field is updated to indicate that it has been processed.
The IdempotentDomainEventHandler class has been implemented to handle idempotence when processing events. This class checks if an event has already been processed by the given consumer, and if so, it skips processing the event again. This helps to ensure that events are not processed multiple times, which is important for maintaining consistency in a distributed system.


