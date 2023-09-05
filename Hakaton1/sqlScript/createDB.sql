-- "Hackaton".companies definition

-- Drop table

-- DROP TABLE companies;

CREATE TABLE companies (
	id int4 NOT NULL DEFAULT nextval('"Hackaton".company_id_seq'::regclass),
	name_company varchar NULL,
	CONSTRAINT company_pk PRIMARY KEY (id)
);


-- "Hackaton".model_car definition

-- Drop table

-- DROP TABLE model_car;

CREATE TABLE model_car (
	id serial4 NOT NULL,
	name_model varchar NULL,
	tonnage numeric NULL,
	CONSTRAINT model_car_pk PRIMARY KEY (id)
);


-- "Hackaton".roles definition

-- Drop table

-- DROP TABLE roles;

CREATE TABLE roles (
	id serial4 NOT NULL,
	name_role varchar NOT NULL,
	CONSTRAINT roles_pk PRIMARY KEY (id)
);


-- "Hackaton".sensors definition

-- Drop table

-- DROP TABLE sensors;

CREATE TABLE sensors (
	id serial4 NOT NULL,
	code varchar NULL,
	CONSTRAINT sensor_pk PRIMARY KEY (id)
);


-- "Hackaton".status definition

-- Drop table

-- DROP TABLE status;

CREATE TABLE status (
	id int4 NOT NULL DEFAULT nextval('"Hackaton".state_order_id_state_order_seq'::regclass),
	name_status varchar NOT NULL,
	CONSTRAINT state_order_pk PRIMARY KEY (id)
);


-- "Hackaton".type_weight definition

-- Drop table

-- DROP TABLE type_weight;

CREATE TABLE type_weight (
	id serial4 NOT NULL,
	name_type_weight varchar NULL,
	CONSTRAINT type_weight_pk PRIMARY KEY (id)
);


-- "Hackaton".car definition

-- Drop table

-- DROP TABLE car;

CREATE TABLE car (
	id serial4 NOT NULL,
	number_car varchar NOT NULL,
	id_model_car int4 NULL,
	CONSTRAINT car_pk PRIMARY KEY (id),
	CONSTRAINT car_model_car_fk FOREIGN KEY (id_model_car) REFERENCES model_car(id)
);


-- "Hackaton".coordinates definition

-- Drop table

-- DROP TABLE coordinates;

CREATE TABLE coordinates (
	id serial4 NOT NULL,
	y varchar NOT NULL,
	x varchar NULL,
	id_sensor int4 NULL,
	is_actual bool NULL,
	weight numeric NULL,
	id_type_wieght int4 NULL,
	CONSTRAINT coordinates_pk PRIMARY KEY (id),
	CONSTRAINT coordinates_fk FOREIGN KEY (id_sensor) REFERENCES sensors(id),
	CONSTRAINT coordinates_type_wieght FOREIGN KEY (id_type_wieght) REFERENCES type_weight(id)
);


-- "Hackaton".product definition

-- Drop table

-- DROP TABLE product;

CREATE TABLE product (
	id serial4 NOT NULL,
	name_product varchar NOT NULL,
	id_sensor int4 NULL,
	CONSTRAINT product_pk PRIMARY KEY (id),
	CONSTRAINT product_fk FOREIGN KEY (id_sensor) REFERENCES sensors(id)
);


-- "Hackaton".users definition

-- Drop table

-- DROP TABLE users;

CREATE TABLE users (
	id serial4 NOT NULL,
	last_name varchar NOT NULL,
	first_name varchar NOT NULL,
	patronymic varchar NOT NULL,
	login varchar NULL,
	"password" varchar NULL,
	id_role int4 NOT NULL,
	CONSTRAINT users_pk PRIMARY KEY (id),
	CONSTRAINT users_roles_fk FOREIGN KEY (id_role) REFERENCES roles(id)
);


-- "Hackaton".car_user definition

-- Drop table

-- DROP TABLE car_user;

CREATE TABLE car_user (
	id serial4 NOT NULL,
	id_car int4 NOT NULL,
	id_user int4 NOT NULL,
	CONSTRAINT car_user_pk PRIMARY KEY (id),
	CONSTRAINT car_user_fk FOREIGN KEY (id_car) REFERENCES car(id),
	CONSTRAINT user_fk FOREIGN KEY (id_user) REFERENCES users(id)
);


-- "Hackaton"."order" definition

-- Drop table

-- DROP TABLE "order";

CREATE TABLE "order" (
	id int4 NOT NULL DEFAULT nextval('"Hackaton".state_order_id_state_order_seq'::regclass),
	number_pass varchar NULL,
	id_company int4 NOT NULL,
	id_car_user int4 NULL,
	date_start date NULL,
	date_end date NULL,
	is_finished bool NULL,
	CONSTRAINT order_pk PRIMARY KEY (id),
	CONSTRAINT order_car_user_fk FOREIGN KEY (id_car_user) REFERENCES car_user(id),
	CONSTRAINT order_company_fk FOREIGN KEY (id_company) REFERENCES companies(id)
);


-- "Hackaton".order_products definition

-- Drop table

-- DROP TABLE order_products;

CREATE TABLE order_products (
	id int4 NOT NULL DEFAULT nextval('"Hackaton".order_product_id_seq'::regclass),
	id_product int4 NOT NULL,
	id_order int4 NOT NULL,
	weight numeric NOT NULL,
	id_type_weight int4 NOT NULL,
	CONSTRAINT order_product_pk PRIMARY KEY (id),
	CONSTRAINT order_fk FOREIGN KEY (id_order) REFERENCES "order"(id),
	CONSTRAINT product_fk FOREIGN KEY (id_product) REFERENCES product(id),
	CONSTRAINT type_weight_fk FOREIGN KEY (id_type_weight) REFERENCES type_weight(id)
);


-- "Hackaton".status_order definition

-- Drop table

-- DROP TABLE status_order;

CREATE TABLE status_order (
	id serial4 NOT NULL,
	id_status int4 NULL,
	id_order int4 NULL,
	date_start timestamptz NULL,
	date_end timestamptz NULL,
	CONSTRAINT status_order_pk PRIMARY KEY (id),
	CONSTRAINT status_order__order_fk FOREIGN KEY (id_order) REFERENCES "order"(id),
	CONSTRAINT status_order_status_fk FOREIGN KEY (id_status) REFERENCES status(id)
);

-----------------------------------------
-- "Hackaton".view_cars source

CREATE OR REPLACE VIEW "Hackaton".view_cars
AS SELECT cu.id,
    cu.id_car,
    mc.name_model,
    mc.tonnage,
    cu.id_user,
    concat(u.last_name, ' ', u.first_name, ' ', u.patronymic) AS "FIO"
   FROM "Hackaton".car_user cu
     JOIN "Hackaton".car c ON cu.id_car = c.id
     JOIN "Hackaton".users u ON cu.id_user = u.id
     JOIN "Hackaton".model_car mc ON c.id_model_car = mc.id;


-- "Hackaton".view_coordins source

CREATE OR REPLACE VIEW "Hackaton".view_coordins
AS SELECT p.name_product,
    c.x,
    c.y,
    s.code,
    c.is_actual,
    c.weight,
    tw.name_type_weight
   FROM "Hackaton".sensors s
     JOIN "Hackaton".coordinates c ON c.id_sensor = s.id
     JOIN "Hackaton".product p ON p.id_sensor = s.id
     JOIN "Hackaton".type_weight tw ON tw.id = c.id_type_wieght;


-- "Hackaton".view_list_checkpoint source

CREATE OR REPLACE VIEW "Hackaton".view_list_checkpoint
AS SELECT st.id AS id_order_status,
    ord.number_pass,
    ord.date_start AS date_start_order,
    ord.date_end AS date_end_order,
    s.name_status,
    s.id AS id_status,
    cr.number_car,
    mc.name_model,
    com.name_company,
    concat(us.last_name, ' ', us.first_name, ' ', us.patronymic) AS "FIO"
   FROM "Hackaton".status_order st
     JOIN "Hackaton"."order" ord ON ord.id = st.id_order
     JOIN "Hackaton".status s ON st.id_status = s.id
     JOIN "Hackaton".car_user cu ON ord.id_car_user = cu.id
     JOIN "Hackaton".car cr ON cu.id_car = cr.id
     JOIN "Hackaton".model_car mc ON mc.id = cr.id_model_car
     JOIN "Hackaton".companies com ON com.id = ord.id_company
     JOIN "Hackaton".users us ON cu.id_user = us.id
  WHERE st.id_status = 2 AND st.date_end = '-infinity'::timestamp with time zone;


-- "Hackaton".view_list_create_pass source

CREATE OR REPLACE VIEW "Hackaton".view_list_create_pass
AS SELECT st.id AS id_order_status,
    ord.number_pass,
    ord.date_start AS date_start_order,
    ord.date_end AS date_end_order,
    cr.number_car,
    mc.name_model,
    com.name_company,
    concat(us.last_name, ' ', us.first_name, ' ', us.patronymic) AS "FIO"
   FROM "Hackaton".status_order st
     JOIN "Hackaton"."order" ord ON ord.id = st.id_order
     JOIN "Hackaton".status s ON st.id_status = s.id
     JOIN "Hackaton".car_user cu ON ord.id_car_user = cu.id
     JOIN "Hackaton".car cr ON cu.id_car = cr.id
     JOIN "Hackaton".model_car mc ON mc.id = cr.id_model_car
     JOIN "Hackaton".companies com ON com.id = ord.id_company
     JOIN "Hackaton".users us ON cu.id_user = us.id
  WHERE ord.number_pass::text = ''::text;


-- "Hackaton".view_list_orders source

CREATE OR REPLACE VIEW "Hackaton".view_list_orders
AS SELECT o.id,
    o.number_pass,
    o.date_start,
    o.date_end,
    c.name_company,
    concat(u.last_name, ' ', u.first_name, ' ', u.patronymic) AS fio,
    mc.name_model,
    p.name_product,
    op.weight,
    tw.name_type_weight,
    o.is_finished
   FROM "Hackaton"."order" o
     JOIN "Hackaton".companies c ON c.id = o.id_company
     JOIN "Hackaton".car_user cu ON cu.id = o.id_car_user
     JOIN "Hackaton".car ca ON ca.id = cu.id_car
     JOIN "Hackaton".model_car mc ON mc.id = ca.id_model_car
     JOIN "Hackaton".users u ON u.id = cu.id_user
     JOIN "Hackaton".order_products op ON op.id_order = o.id
     JOIN "Hackaton".product p ON p.id = op.id_product
     JOIN "Hackaton".type_weight tw ON tw.id = op.id_type_weight;


-- "Hackaton".view_to_warehouse source

CREATE OR REPLACE VIEW "Hackaton".view_to_warehouse
AS SELECT st.id,
    st.date_start,
    s.name_status,
    ord.number_pass,
    cr.number_car,
    mc.name_model,
    pr.name_product,
    op.weight,
    concat(us.last_name, ' ', us.first_name, ' ', us.patronymic) AS "FIO"
   FROM "Hackaton".status_order st
     JOIN "Hackaton".status s ON st.id_status = s.id
     JOIN "Hackaton"."order" ord ON st.id_order = ord.id
     JOIN "Hackaton".car_user cu ON ord.id_car_user = cu.id
     JOIN "Hackaton".car cr ON cu.id_car = cr.id
     JOIN "Hackaton".order_products op ON ord.id = op.id_order
     JOIN "Hackaton".product pr ON pr.id = op.id_product
     JOIN "Hackaton".model_car mc ON mc.id = cr.id_model_car
     JOIN "Hackaton".users us ON cu.id_user = us.id;