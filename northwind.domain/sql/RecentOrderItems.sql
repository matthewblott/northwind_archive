select
  d.Id, d.OrderId, d.ProductId, d.UnitPrice, d.Quantity
from Orders o join main.OrderDetails d on o.Id = d.OrderId
where CustomerId = @customerId
limit 5