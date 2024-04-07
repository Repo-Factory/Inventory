GRANT ALL PRIVILEGES ON DATABASE docker_postgres TO docker_postgres;

CREATE TABLE inventory (
  id          serial PRIMARY KEY,
  name        varchar(20),
  buy_price   DECIMAL(10, 2),
  sell_price  DECIMAL(10, 2)
);

insert into employees (name, buy_price, sell_price) values 
  (tostitos, 15, 18), 
  (crepas, 12, 25), 
  (mamut, 2, 3);
