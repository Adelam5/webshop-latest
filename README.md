# Webshop
## Web application: .Net 7 & React

### Description
This webshop application allows users to register, confirm their email, and login to browse and purchase products. Users can view a list of available products and see detailed information about each product. After logging in, users can add products to their cart and proceed to checkout, where they can review their order details and choose delivery method. The application also features integration with Stripe for secure payment processing. After completing the checkout process, users can view their order history on their profile page.  

Application is deployed on free layer of railway: https://webshop.up.railway.app/  
More features are available on backend, and can be tested with postman: https://documenter.getpostman.com/view/24457768/2s93ebUX5V

Payment process can be tested with stripe test card numbers. Eg. Success: 4242 4242 4242 4242, Failure: 4000 0000 0000 0002


### Architecture Overview
This project was built using a Clean Architecture approach, with a focus on Domain-Driven Design (DDD) and Command Query Responsibility Segregation (CQRS) patterns. The Clean Architecture separates the codebase into layers, with each layer having a specific responsibility and dependency direction. The layers include the Domain layer, Application layer, Infrastructure layer, and Presentation layer. This architecture ensures a clear separation of concerns, making the codebase more modular and easier to maintain and test.

The DDD approach was used to model the application's domain entities, aggregates, and value objects, ensuring that the business logic is encapsulated within the domain layer. The CQRS pattern was used to separate the read and write operations, with the use of separate commands and queries to modify and retrieve data from the system. Entity Framework Core was used for command operations (i.e., write operations), while Dapper was used for query operations (i.e., read operations).

Domain events are used to represent important state changes within the domain, and can be used to trigger side-effects or updates in other parts of the system. Domain events are used to maintain consistency between different parts of the system. For example, when a customer is deleted, a domain event is raised to also delete the user with relation to this customer.

The project implements an Outbox pattern to ensure reliable messaging between services. When a write operation is executed in the application, an event is generated and stored in the Outbox table. A background job processes the Outbox messages from the database and publishes them to a message broker using a retry policy. This approach ensures that events are reliably delivered, even if the message broker is temporarily unavailable.  
[Read Additional Notes...](Notes.md)

### Project Features
#### Features implemented on frontend and backend:
- User registration
- Email and account verification
- User login
- List products
- View product details
- Add and remove products to/from cart
- Checkout
- View user details and order history

#### Features only on backend:
- Forgot password reset (reset password link sent to user email)
- Change password, update user personal details 
- CRUD functionalities for Products and Customers

### Built With
#### Database:

- PostgreSQL

#### Backend:

- .Net 7
- Serilog
- Fluent Validation
- MediatR
- Entity Framework Core
- Dapper
- Sendgrid
- AWSSDK.S3
- Quartz
- Polly
- Stripe.net

#### Frontend:

- ReactJS
- React Router Dom
- React Query
- Zustand
- Formik
- Yup
- Mui & styled components
- Stripe
