
# Sipay Final Project 👩🏻‍💻
- What is the aim of this project?
This project is the Site Management API Project, and our expectations for this project are as follows:

✓ It includes an authentication system with roles assigned to our Manager (Admin) and Users. The Manager is responsible for billing the users on a monthly basis, and through our services, users will be able to receive and record their assigned payments in the system.

## ✍🏻 Let's explain the architecture of our project:

- Base Layer: As the name suggests, this is the layer where we keep the common parts of our project.

- Entity Layer: This layer contains the models we use in our project. It includes classes such as Apartment, Bank, Building, Messages, MonthlyInvoice, Payment, User, and UserLog for tracking user entries.

- Data Layer: The Data Layer manages the communication with the database and includes our DbContext class. As we are using the code-first approach, we also place our migrations here. (We'll explain the UnitOfWork class in detail here as well).

- Business Layer: The Business Layer is where the core business logic of the application resides. This layer handles database operations, business rules, calculations, and other related tasks.

- Schema Layer: The Schema Layer includes the Request-Response classes that will facilitate communication. It also houses the MapperConfiguration.

- API Layer: The API Layer serves as an interface that allows different applications or systems to interact with our application.

By organizing our project into these distinct layers, we achieve modularity, separation of concerns, and maintainability, making it easier to develop and manage the application.

## Projemizin işleyişini anlatmaya başlayabiliriz,
In this project, we emphasize asynchronous programming and generic structures to improve code efficiency and follow clean code principles. We use **ApiResponse<T>** to handle API call results uniformly. Asynchronous programming enhances responsiveness and scalability, while generics enable reusable code for different data types. 
```c#
 public partial class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Response { get; set; }
    }
 ```

Example ,
```
 public async Task<ApiResponse<List<XResponse>>> GetAllAsync()
``` 

❓ As we continue with the project, let me explain why we have two generic structures, GenericRepository and GenericService, and what distinguishes them:
- GenericRepository: It is designed to handle database access and data operations. By applying CRUD (Create, Read, Update, Delete) operations in a generic manner, it allows us to avoid repetitive coding for multiple database tables. The GenericRepository abstracts database operations in a way that is independent of the specific entity types, making it reusable across different data models.
```c#
 public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SPDbContext _dbContext;

        public GenericRepository(SPDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
      
        public async Task UpdateAsync(T t)
        {
            _dbContext.Set<T>().Update(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }

        .
        .
        .
        
 ```

- GenericService: This structure is responsible for implementing business logic and is used in the business layer. Instead of focusing on database operations, it contains methods that implement the business logic. The GenericService provides a way to perform common business operations once and avoid repeating the same logic for different entity types.

 ```c#
  public class GenericService<T, TRequest, TResponse> : IGenericService<T, TRequest, TResponse> where T : class
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public GenericService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }
        public async Task<ApiResponse<TResponse>> GetById(int id, params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetByIdWithIncludeAsync(id, includes);
                var mapped = mapper.Map<T, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetById");
                return new ApiResponse<TResponse>(ex.Message);
            }
        }
        public async Task<ApiResponse<List<TResponse>>> GetAll(params string[] includes)
        {
            try
            {
                var entity = await unitOfWork.DynamicRepo<T>().GetAllWithIncludeAsync(includes);
                var mapped = mapper.Map<List<T>, List<TResponse>>(entity);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "GenericService.GetAll");
                return new ApiResponse<List<TResponse>>(ex.Message);
            }
        }
        .
        .
        .
 ```

❓ Unit Of Work 

UnitOfWork (UoW) allows you to aggregate changes made within a transaction or a series of operations and ensures the consistency of database operations.
```c#
  public IGenericRepository<T> DynamicRepo<T>() where T : class
        {
            return new GenericRepository<T>(_dbContext);
        }
 ```
Thanks to DynamicRepo<T>, we can perform database operations with a single method instead of repeatedly writing the same database operations for different entity types.

## Login with Token 🎟
We are writing a service for the token.
Let's first create its interface
```c#
 public interface ITokenService
    {
        public ApiResponse<TokenResponse> CreateToken(TokenRequest tokenRequest);
    }
 ```
 Request Validation: Firstly, it checks whether the incoming request is null or if the username and password are empty.
 ```c#
  public class TokenService : ITokenService

  public ApiResponse<TokenResponse> CreateToken(TokenRequest request)
        {
            if (request is null)
            {
                return new ApiResponse<TokenResponse>("Request was null");
            }
            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
            {
                return new ApiResponse<TokenResponse>("Request was null");
            }

            request.UserName = request.UserName.Trim().ToLower();
            request.Password = request.Password.Trim();
 ```

User Authentication: The user's identity is verified by checking if the user exists in the database and if the provided password is correct.

User Status Check: The user's status is checked. If the user's status is inactive (status=2), an ApiResponse<TokenResponse> response with an appropriate error message is returned.

Password Retry Count Check: If the user enters the wrong password three times, their status is updated to inactive (status=2).

```c#
    var user = unitOfWork.DynamicRepo<User>().Where(x => x.UserName.Equals(request.UserName)).FirstOrDefault();
            if (user is null)
            {
                Log(request.UserName, LogType.InValidUserName);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }
            if (user.Password.ToLower() != CreateMD5(request.Password))
            {
                user.PasswordRetryCount++;
                

                if (user.PasswordRetryCount > 3)
                    user.Status = 2;

                unitOfWork.DynamicRepo<User>().UpdateAsync(user);
                unitOfWork.SaveChangesAsync();

                Log(request.UserName, LogType.WrongPassword);
                return new ApiResponse<TokenResponse>("Invalid user informations");
            }


            if (user.Status != 1)
            {
                Log(request.UserName, LogType.InValidUserStatus);
                return new ApiResponse<TokenResponse>("Invalid user status");
            }
            if (user.PasswordRetryCount > 3)
            {
                Log(request.UserName, LogType.PasswordRetryCountExceded);
                return new ApiResponse<TokenResponse>("Password retry count exceded");
            }

           
            user.Status = 1;


            unitOfWork.DynamicRepo<User>().UpdateAsync(user); 
            unitOfWork.SaveChangesAsync();


 ```

Creating Access Token: After successful user authentication, an access token is generated for the user. This object includes the username, access token, and the expiration date of the token.

```c#
string token = Token(user);

            Log(request.UserName, LogType.LogedIn);

            TokenResponse response = new()
            {
                AccessToken = token,
                ExpireTime = DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                UserName = user.UserName
            };

            return new ApiResponse<TokenResponse>(response);
 ```
This code snippet creates a JWT (JSON Web Token) for the user. JWT is a token used for user authentication. It contains the user's identity (username), the access token, and the expiration date of the token. This access token signifies that the user's authentication was successful and can be used in authorization processes.
```c#
    private string Token(User user)
        {
            Claim[] claims = GetClaims(user);
            var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);
                var jwtToken = new JwtSecurityToken(
                jwtConfig.Issuer,
                jwtConfig.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return accessToken;
        }


 ```

After preparing our service, we configure the necessary Jwt settings in the appsettings.json file,
```c#
  "JwtConfig": {
    "Secret": "BeHappyMakeHappy",
    "Issuer": "SP",
    "Audience": "SP",
    "AccessTokenExpiation": 100
  },
 ```
- "Secret": Represents the secret key used to sign the JWT for security purposes.
- "Issuer": Indicates the issuer of the JWT, which is typically the name or identity of the application.
- "Audience": Specifies the intended audience for the JWT, i.e., the application or service that can consume it.
- "AccessTokenExpiration": Defines the expiration time for the access token in minutes after which it becomes invalid, requiring re-authentication.

Ardından startup.cs açıyoruz ve burada JwtConfig'imizi geçiyoruz;
```c#
public static JwtConfig JwtConfig { get; private set; }
 JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));
 ```

We are configuring authentication and authorization settings.
```c#
services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                    ValidAudience = JwtConfig.Audience,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
            });
 ```
🔒 So what is using tokens? Let's explain by giving an example from our project.  🔒

Let's explain how to obtain a token from the AppUserController for the two users defined in the database: "admin" and "user".

```c#
        [HttpPost("AdminLogin")]
        public ApiResponse<TokenResponse> AdminLogin([FromBody] TokenRequest request)
        {
            // You can implement admin login logic here
            var response = _tokenService.CreateToken(request);
            return response;
        }
 ```
- Let's first enter incorrect information,
```c#
{
  "userName": "string",
  "password": "string"
}
```
The error I encountered is:
Code: 401
Details: Unauthorized
Error: Unauthorized. The 401 error indicates that a client attempted to access a resource without authentication credentials or with invalid authentication credentials.

Now, when I log in with a registered user in the database, I should receive a 200 response, and you should have obtained a structure similar to the following,
```json
{
  "success": true,
  "message": "Success",
  "response": {
    "expireTime": "2023-08-06T20:17:21.6647694+03:00",
    "accessToken": "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLIsI..",
    "userName": "Admin"
  }

 ```
We can determine the places where users or specifically "users" can access using the notation **[Authorize(Roles = "Admin")]**.
After the login process, the admin can assign a user by providing a UserName and Password. The assigned user can then log in using the provided credentials.;
```c#
 [HttpPost]
        public async Task<ApiResponse> UserCreate([FromBody] UserRequest request)
        {
            string hashedPassword = CalculateMD5Hash(request.Password);
            request.Password = hashedPassword;

            var response = await _service.Insert(request); // UserService servisindeki Insert metodu veritabanına kayıt ediyor.

            return response;
        }

  }

 ```

One important thing to note here is that when we enter our password, it is encrypted using MD5 before being stored in the database.,
 ```c#
    private string CalculateMD5Hash(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
  }
 ```

## 📝 Now that we have completed the login process let's briefly explain how the system works.
- For example, let's perform an addition and payment process from scratch to understand how it works.

ApartmentController içerisinde CRUD işlemleri bulunmaktadır mesela sıfırdan bir kullanıcıya daire atayalım,

```c#
  [HttpPost]
        public async Task<ApiResponse> AddApartment([FromBody] ApartmentRequest request)
     {
         var response = await _service.Insert(request);  
         return response;
     }
 ```
```json
{
  "userId": 1,
  "buildingId": 1,
  "isOccupied": true,
  "isOwner": true,
  "type": "2+1",
  "blockName": "A Blok",
  "floorNumber": 1,
  "apartmentNumber": 1
} 
 ```
Let's assume that we are making an invoice entry for this apartment,
```c#
     [HttpPost]
        public async Task<ActionResult<ApiResponse>> AddMonthlyInvoice([FromBody] MonthlyInvoiceRequest request)
        {
            var response = await _monthlyInvoiceService.Insert(request);
            return response;
        }

 ```
 ```json
{
  "monthlyInvoiceId": 1,
  "invoiceAmount": 10,
  "date": "2023-08-06T18:10:20.948Z",
  "apartment": {
    "userId": 2,
    "buildingId": 1,
    "floorNumber": 1,
    "apartmentNumber": 1
  }
}
 ```
We then use the service we have written for the payment transaction.
```c#
 public async Task<ApiResponse<TransferReponse>> PayAsync(CashRequest request)
        {
            if (request == null)
                return new ApiResponse<TransferReponse>("Invalid Request");

            if (request.CreditCardNumber == null || request.CreditCardNumber.Length != 19)
                return new ApiResponse<TransferReponse>("Invalid Credit Card Number");

            // Validate CVV
            if (request.CVV == null || request.CVV.Length != 3)
                return new ApiResponse<TransferReponse>("Invalid CVV");

            // Validate expiration date
            if (request.ExpirationDate == null || request.ExpirationDate.Length != 5)
                return new ApiResponse<TransferReponse>("Invalid Expiration Date");


            User userAccount = await _unitOfWork.DynamicRepo<User>().GetByIdAsync(request.UserId);

            if (userAccount == null)
                return new ApiResponse<TransferReponse>("Invalid User Account");


            MonthlyInvoice monthlyInvoice = await _unitOfWork.DynamicRepo<MonthlyInvoice>().GetByIdAsync(request.MonthlyInvoiceId);


            if (monthlyInvoice == null)
                return new ApiResponse<TransferReponse>("User does not have any monthly invoices");


            Apartment apartment = await _unitOfWork.DynamicRepo<Apartment>().GetByIdAsync(request.MonthlyInvoiceId);


            if (apartment.IsOccupied == false)
                return new ApiResponse<TransferReponse>("Daire boş"); 



                    Payment payment = new Payment
            {
                UserId = userAccount.UserId,
                MonthlyInvoiceId = monthlyInvoice.MonthlyInvoiceId,
                PaymentDate = DateTime.Now,
                InvoiceAmount = request.Amount,
                Balance = userAccount.Balance - request.Amount,

                Message = "Ödeme başarıyla gerçekleştirildi.",
                NewBalance = userAccount.Balance - request.Amount
            };
            userAccount.Balance -= request.Amount;

            await _unitOfWork.DynamicRepo<Payment>().InsertAsync(payment);
            await _unitOfWork.DynamicRepo<User>().UpdateAsync(userAccount);
            await _unitOfWork.SaveChangesAsync();


            PaymentResponse paymentResponse = new PaymentResponse
            {
                PaymentId = payment.Id,

                Message = "Payment Successful",
                NewBalance = userAccount.Balance,
                PaymentDate = DateTime.Now
            };


            return new ApiResponse<TransferReponse>("Payment Successful");
        }

}
 ```
The incoming CashRequest parameter is being checked. If it is empty, an "Invalid Request" error is returned. Payment information, such as credit card number, CVV, and expiration date, is validated. If any of them are invalid, an appropriate error message is returned.

User account (User) and monthly invoice (MonthlyInvoice) information is retrieved from the database. If the user account or monthly invoice cannot be found, the process is terminated with the relevant error messages.

The status of the apartment is checked, and if it is vacant, the transaction is terminated.

Payment details (Payment) are created, and the payment amount is deducted from the user's account to calculate the new balance. These transactions are then stored in the database.

A response indicating a successful payment is generated and sent back as the return value.

In general, this code block represents a function that performs a payment transaction using the provided payment information from the user and informs the user about the success of the transaction along with the updated account balance.

In brief, our system operates as follows  🧡 👩‍🦰
## Message passing with RabbitMQ 📩
❓ RabbitMQ is a message broker software that enables communication between different applications by facilitating the exchange of messages. It acts as an intermediary to ensure seamless message passing between systems, allowing them to work asynchronously and independently.

First, we need to set up RabbitMQ using Docker;
```c#
docker pull rabbitmq:latest
docker run -d --name my_rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:latest
 ```
app.setting.json
```json
  "RabbitMqConfiguration": {
    "RabbitMqConnection":  "localhost"
  }
}


 ```
 First, we use the _configuration object to retrieve the RabbitMQ connection information. The connection details are fetched from the location RabbitMqConfiguration:RabbitMqConnection.

Using the ConnectionFactory class, we establish a connection to the RabbitMQ server. The HostName property is set to the connectionHost variable, which represents the specified server.

We create a channel to define the queue where messages will be sent. The queue is named "Message" and is set as non-exclusive and non-auto-deleted.

The message to be sent is converted to JSON format using the JsonConvert.SerializeObject() method.

 ```c#
public void SendMessage<T>(T message)
        {
            var connectionHost = _configuration.GetSection("RabbitMqConfiguration:RabbitMqConnection").Value;
            var factory = new ConnectionFactory
            {
                HostName = connectionHost,
            };

            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("Message", exclusive: false, autoDelete: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "Message", body: body);


        }
  }
}
 ```
 
Next, we create the message body and convert it into a byte array using UTF8 encoding.

Finally, we use the channel.BasicPublish() method to send the message to the specified queue. The exchange name is set to an empty string, indicating a direct routing to the queue.

In summary, this code defines a message sending method that sends a message of type T to a RabbitMQ queue.
```c#
[HttpPost]
        [Authorize(Roles = "user")]
        public async Task<ApiResponse<MessagesResponse>> SendMessage([FromBody] MessagesRequest request)
        {
            _rabbitMqProducer.SendMessage(request);

            // Insert the message into the database using the IMessageService.
            var response = await _messageService.UserSendMessageAsync(request);

            return response;
        }
  }
}
 ```
 And in my message controller, I handle the necessary transmissions and save my messages to the database. 🎈

## MailJob 📧
We have a system that sends emails to users who have not paid their invoices every 24 hours. 

This code snippet defines a MailService class that sends reminder emails at regular intervals using a timer and SMTP settings.
```c#
 public class MailService : IMailService
    {
        private readonly System.Timers.Timer _timer;
        private readonly SmtpSettings _smtpSettings;

        public MailService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
            _timer = new System.Timers.Timer(86400000); // 24 hours in milliseconds
            _timer.Elapsed += async (sender, e) => await SendReminderEmail(_smtpSettings.AdminEmail);
            _timer.Start();
        }
 ```

 It contains an asynchronous method named SendReminderEmail that sends payment reminder emails to users. The method uses SMTP settings and .NET Framework classes to handle the email sending process. An email is created with specified subject, content, and recipient email address, and it is sent using an SMTP client. In case of an error, an exception is thrown.
```c#
public async Task SendReminderEmail(string userEmail)
        {
            try
            {
                var fromEmail = _smtpSettings.AdminEmail;
                var subject = "Ödeme Hatırlatması";
                var body = "Ödemenizi yapmayı unutmayınız!";

                var mailMessage = new MailMessage(fromEmail, userEmail, subject, body);
                var smtpClient = new SmtpClient(_smtpSettings.Server, _smtpSettings.Port)
                {
                    Credentials = new System.Net.NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true
                };

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Error while sending email: " + ex.Message);
            }
 ```
And then, we send these emails from the MailController using the **[HttpGet("send-reminders")]** endpoint.


That's it!! I hope it's clear enough. See you soon! 🧡 👩‍🦰
