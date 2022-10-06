# How to test

To test your services you can follow the following recommendations:

- Test the correct cases (Status 200 OK)
  - Test the different cases according to your business rules. For example, for login, you can add tests to test login with different types of users.
  - If you have default values, do not send the default values and check that the default parameters are taken into account.
- Test with incorrect cases (Status 400 Bad Request)
  - Incorrect parameters: For example, sending letters in some integer value. If a field only allows 50 characters send 60 characters.
  - Do not send the required parameters to verify that a 400 status is returned and a clear error message is displayed.
  - If there are limits on some value, for example a key that is between 1 and 99, send a negative value or a value greater than 99.
- Testing a service without authorization (Status 401 Unauthorized)
  - Test a service that requires authorization to access information
- Try a service where you do not have permission (Status 403 Forbidden)
  - You can test a service where the user does not have access to that information
- Test with data that does not return data (Status 404 Not Found or with the status defined for this case, it can be a status 200 with an empty json or a status 400)
  - For example, in the case of clients, you can try sending a client id that does not exist to test that it returns a 404 status. Some considering the 404 status only for incorrect urls, others considering them when a resource does not exist.

# Collections and Code

- You can structure your collections by module -> Sevice
- Use environment variables to test with different servers QA, State, Prod
- Try to reuse function 
- Use CSV data when you need to test the same service with different values
- Document all business rules
