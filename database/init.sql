GRANT ALL PRIVILEGES ON DATABASE docker_postgres TO docker_postgres;

CREATE TABLE inventory (
  id          serial PRIMARY KEY,
  name        VARCHAR(20),
  cost_basis  DECIMAL(10, 2),
  sell_price  DECIMAL(10, 2),
  quantity    INT
);

CREATE TABLE sales (
  id          serial PRIMARY KEY,
  name        VARCHAR(20),
  time        DATETIME,
  sell_price  DECIMAL(10,2)
);

