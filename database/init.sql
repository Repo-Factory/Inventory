GRANT ALL PRIVILEGES ON DATABASE docker_postgres TO docker_postgres;

CREATE TABLE inventory (
  Id          serial PRIMARY KEY,
  Name        VARCHAR(20),
  Cost_basis  DECIMAL(10, 2),
  Quantity    INT
);

CREATE TABLE sales (
  Id          serial PRIMARY KEY,
  Name        VARCHAR(20),
  Time        TIMESTAMP,
  Sell_price  DECIMAL(10,2)
);

