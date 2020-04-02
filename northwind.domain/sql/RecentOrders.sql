select Id, CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate
from Orders 
where CustomerId = @customerId
limit 5