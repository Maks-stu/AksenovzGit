--
-- PostgreSQL database dump
--

\restrict r6PgI40gvNehs3IREQGu3L1idh1uIcbEa411FbxoiF5et8wckEZfFfLWbYAq8QH

-- Dumped from database version 15.14
-- Dumped by pg_dump version 15.14

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: app; Type: SCHEMA; Schema: -; Owner: app
--

CREATE SCHEMA app;


ALTER SCHEMA app OWNER TO app;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: partners; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.partners (
    id integer NOT NULL,
    name character varying(255) NOT NULL,
    legal_address character varying(255) NOT NULL,
    inn character varying(255) NOT NULL,
    director_name character varying(255),
    phone character varying(15),
    email character varying(255),
    logo character varying(255),
    rating integer DEFAULT 1,
    type character varying
);


ALTER TABLE app.partners OWNER TO app;

--
-- Name: partners_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.partners_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.partners_id_seq OWNER TO app;

--
-- Name: partners_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.partners_id_seq OWNED BY app.partners.id;


--
-- Name: production; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.production (
    id integer NOT NULL,
    article character varying(255) NOT NULL,
    name character varying(255) NOT NULL,
    description character varying(255),
    photo character varying(255),
    min_partner_price numeric(10,2) NOT NULL,
    length integer,
    width integer,
    height integer,
    net_weight numeric(10,2),
    gross_weight numeric(10,2),
    quality_certificate character varying(255),
    standard_number character varying(255),
    cost_price numeric(10,2)
);


ALTER TABLE app.production OWNER TO app;

--
-- Name: production_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.production_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.production_id_seq OWNER TO app;

--
-- Name: production_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.production_id_seq OWNED BY app.production.id;


--
-- Name: products_sales; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.products_sales (
    id integer NOT NULL,
    partner_id integer NOT NULL,
    production_id integer NOT NULL,
    count numeric(12,2) NOT NULL,
    date date NOT NULL,
    total_price numeric(12,2)
);


ALTER TABLE app.products_sales OWNER TO app;

--
-- Name: products_sales_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.products_sales_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.products_sales_id_seq OWNER TO app;

--
-- Name: products_sales_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.products_sales_id_seq OWNED BY app.products_sales.id;


--
-- Name: selling_places; Type: TABLE; Schema: app; Owner: app
--

CREATE TABLE app.selling_places (
    id integer NOT NULL,
    name character varying(255),
    type character varying(255),
    address character varying(255),
    partner_id integer
);


ALTER TABLE app.selling_places OWNER TO app;

--
-- Name: selling_places_id_seq; Type: SEQUENCE; Schema: app; Owner: app
--

CREATE SEQUENCE app.selling_places_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE app.selling_places_id_seq OWNER TO app;

--
-- Name: selling_places_id_seq; Type: SEQUENCE OWNED BY; Schema: app; Owner: app
--

ALTER SEQUENCE app.selling_places_id_seq OWNED BY app.selling_places.id;


--
-- Name: partners id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partners ALTER COLUMN id SET DEFAULT nextval('app.partners_id_seq'::regclass);


--
-- Name: production id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.production ALTER COLUMN id SET DEFAULT nextval('app.production_id_seq'::regclass);


--
-- Name: products_sales id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products_sales ALTER COLUMN id SET DEFAULT nextval('app.products_sales_id_seq'::regclass);


--
-- Name: selling_places id; Type: DEFAULT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.selling_places ALTER COLUMN id SET DEFAULT nextval('app.selling_places_id_seq'::regclass);


--
-- Data for Name: partners; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.partners (id, name, legal_address, inn, director_name, phone, email, logo, rating, type) FROM stdin;
1	УютДом	625721, г. Тюмень, ул. Парковая, 11, оф. 72	797200381049	Петров М. Д.	+79007477589	aquino_uriel90@mail.ru	\N	7	ООО
3	Билдерус	625488, г. Тюмень, ул. Интернациональная, 8, оф. 84	201133352584	Данилова А. Н.	+79540518536	chamberlain_theadore1@mail.ru	\N	4	ООО
4	ХрамМастеров	625226, г. Тюмень, ул. Пролетарская, 48, оф. 52	568556813757	Белов А. М.	+79665929385	bernard-ernestine33@mail.ru	\N	7	ООО
2	СтройТочка	603984, г. Нижний Новгород, ул. Северная, 36, оф. 86	152267001050	Григорьева Д. Б.	+79391604017	gamez-nikki89@mail.ru	\N	7	ИП
\.


--
-- Data for Name: production; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.production (id, article, name, description, photo, min_partner_price, length, width, height, net_weight, gross_weight, quality_certificate, standard_number, cost_price) FROM stdin;
1	10000001	Ламинат 33 класс	Высококачественный ламинат для жилых помещений	laminat1.jpg	15.50	1200	200	8	12.50	14.00	QC12345	SN6789	8.75
2	10000002	Пробковое покрытие	Эко-материал для домашнего использования	cork1.jpg	20.00	900	900	2	8.00	8.50	QC23456	SN7890	10.50
3	10000003	Ковровое покрытие	Теплый и мягкий ковролин для офисов	carpet1.jpg	12.75	2000	300	1	20.00	21.00	QC34567	SN8901	14.30
4	10000004	Паркетная доска	Древесиновый пол для дорогих интерьеров	parquet1.jpg	25.00	1800	150	3	10.00	11.00	QC45678	SN9012	18.50
5	10000005	Виниловое покрытие	Удобное для влажных помещений покрытие	vinyl1.jpg	13.40	1200	250	0	9.00	9.50	QC56789	SN0123	7.80
6	10000006	Каменное покрытие	Эстетичный и прочный материал	stone1.jpg	30.00	1500	150	1	25.00	26.00	QC67890	SN1234	22.00
7	10000007	Ковролин для спальни	Мягкое покрытие для утренних пробуждений	bedcarpet1.jpg	18.00	2000	250	1	19.00	20.00	QC78901	SN2345	13.50
8	10000008	Линолеум бытовой	Экономичный вариант для частных домов	linoleum1.jpg	9.50	1500	600	0	13.00	13.50	QC89012	SN3456	6.50
\.


--
-- Data for Name: products_sales; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.products_sales (id, partner_id, production_id, count, date, total_price) FROM stdin;
1	1	1	1100.00	2026-01-05	12100.00
2	2	2	2500.00	2026-01-10	62500.00
3	3	3	3500.00	2026-01-15	12450.00
4	4	4	4000.00	2026-01-20	74000.00
5	1	5	1500.00	2026-02-01	11700.00
6	2	6	5000.00	2026-02-05	110000.00
7	3	7	2000.00	2026-02-10	27000.00
8	4	8	6000.00	2026-02-15	39000.00
9	1	2	1200.00	2026-02-20	24000.00
10	2	3	1300.00	2026-02-25	4950.00
11	3	4	1400.00	2026-03-01	25900.00
12	4	1	1000.00	2026-03-05	15500.00
13	1	3	1150.00	2026-03-07	41450.00
14	2	4	2500.00	2026-01-07	46000.00
15	3	5	1450.00	2026-01-12	17400.00
16	4	6	3000.00	2026-01-18	66000.00
17	1	7	1700.00	2026-02-03	22950.00
18	2	8	4000.00	2026-02-08	26000.00
19	3	1	1250.00	2026-02-13	19375.00
21	1	4	1000.00	2026-02-25	18500.00
22	2	5	1400.00	2026-03-02	19360.00
23	3	6	1500.00	2026-03-08	33000.00
24	4	7	1600.00	2026-03-12	21600.00
25	1	8	1800.00	2026-01-03	11700.00
26	2	1	1250.00	2026-01-09	19375.00
27	3	2	2000.00	2026-01-14	50000.00
28	4	3	2100.00	2026-01-21	7473.00
29	1	4	1000.00	2026-02-02	18500.00
30	4	2	330000.00	1900-01-03	8250000.00
\.


--
-- Data for Name: selling_places; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.selling_places (id, name, type, address, partner_id) FROM stdin;
\.


--
-- Name: partners_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.partners_id_seq', 1, false);


--
-- Name: production_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.production_id_seq', 1, false);


--
-- Name: products_sales_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.products_sales_id_seq', 29, true);


--
-- Name: selling_places_id_seq; Type: SEQUENCE SET; Schema: app; Owner: app
--

SELECT pg_catalog.setval('app.selling_places_id_seq', 1, false);


--
-- Name: partners inn_unique; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partners
    ADD CONSTRAINT inn_unique UNIQUE (inn);


--
-- Name: partners partners_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.partners
    ADD CONSTRAINT partners_pkey PRIMARY KEY (id);


--
-- Name: production production_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.production
    ADD CONSTRAINT production_pkey PRIMARY KEY (id);


--
-- Name: products_sales products_sales_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products_sales
    ADD CONSTRAINT products_sales_pkey PRIMARY KEY (id);


--
-- Name: selling_places selling_places_pkey; Type: CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.selling_places
    ADD CONSTRAINT selling_places_pkey PRIMARY KEY (id);


--
-- Name: selling_places owning; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.selling_places
    ADD CONSTRAINT owning FOREIGN KEY (partner_id) REFERENCES app.partners(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: products_sales product; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products_sales
    ADD CONSTRAINT product FOREIGN KEY (production_id) REFERENCES app.production(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: products_sales recipient; Type: FK CONSTRAINT; Schema: app; Owner: app
--

ALTER TABLE ONLY app.products_sales
    ADD CONSTRAINT recipient FOREIGN KEY (partner_id) REFERENCES app.partners(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict r6PgI40gvNehs3IREQGu3L1idh1uIcbEa411FbxoiF5et8wckEZfFfLWbYAq8QH

