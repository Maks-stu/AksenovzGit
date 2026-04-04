--
-- PostgreSQL database dump
--

\restrict ZJ6rDljrsz4zpRIj7vvWMAVrQTdYnc0OqrxQeAFRfLLFTatJITQmtLypFZhRYGL

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
-- Name: aksenov_16_03; Type: DATABASE; Schema: -; Owner: app
--

CREATE DATABASE aksenov_16_03 WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';


ALTER DATABASE aksenov_16_03 OWNER TO app;

\unrestrict ZJ6rDljrsz4zpRIj7vvWMAVrQTdYnc0OqrxQeAFRfLLFTatJITQmtLypFZhRYGL
\connect aksenov_16_03
\restrict ZJ6rDljrsz4zpRIj7vvWMAVrQTdYnc0OqrxQeAFRfLLFTatJITQmtLypFZhRYGL

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
1	УютДом	'625721, г. Тюмень, ул. Парковая, 11, оф. 72	797200381049	Петров М.Д.	89007477589	aquino_uriel90@mail.ru	Выберите файл...	7	ООО
2	СтройТочка	603984, г. Нижний Новгород, ул. Северная, 36, оф. 86	152267001050	Григорьева Д.Б.	89391604017	gamez-nikki89@mail.ru	Выберите файл...	7	ИП
3	Билдерус	625488, г. Тюмень, ул. Интернациональная, 8, оф. 84	201133352584	Данилова А.Н.	89540518536	chamberlain_theadore1@mail.ru	Выберите файл...	4	ООО
4	Храм Мастеров	625226, г. Тюмень, ул. Пролетарская, 48, оф. 52	568556813757	Белов А.М.	89665929385	bernard-ernestine33@mail.ru	Выберите файл...	7	ООО
5	Строечка	426200, г. Ижевск, ул. Дорожная, 5, оф. 81	3414270102	Федорова Н.А.	89492462223	soxejax_uvu93@mail.ru	Выберите файл...	7	ООО
\.


--
-- Data for Name: production; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.production (id, article, name, description, photo, min_partner_price, length, width, height, net_weight, gross_weight, quality_certificate, standard_number, cost_price) FROM stdin;
1	10000002	Пробковое покрытие	Эко-материал для домашнего использования	Выберите файл...	20.00	900	900	2	8.00	8.50	4546	RU C-DE.1234.A.00123/24	22.70
2	10000004	Паркетная доска	Древесиновый пол для дорогих интерьеров	Выберите файл...	18.50	1800	150	3	10.00	11.00	5	RU C-DE.1234.A.00123/24	25.00
3	10000006	Каменное покрытие	Эстетичный и прочный материал	Выберите файл...	22.00	1500	150	1	25.00	26.00	6	RU C-DE.1234.A.00123/24	30.00
4	10000007	Ковролин для спальни	Мягкое покрытие для утренних пробуждений	Выберите файл...	13.50	2000	250	1	19.00	20.00	7	RU C-DE.1234.A.00123/27	15.00
5	100008	Новый ламинат	ттт	Выберите файл...	15.00	100	100	100	3.00	5.00	8	RU C-DE.1234.A.00123/32	18.00
6	10000001	Ламинат 33 класс	Высококачественный ламинат для жилых помещений	Выберите файл...	8.75	1200	200	8	20.00	21.00	01	RU C-DE.1234.A.01153/01	16.00
7	10000003	Ковровое покрытие	Теплый и мягкий ковролин для офисов	Выберите файл...	13.00	2000	300	1	20.00	21.00	1001503	RU C-DE.1564.A.00312/24	14.30
8	10000005	Виниловое покрытие	Удобное покрытие для влажных помещений	Выберите файл...	7.80	1200	250	1	13.00	13.50	234567	RU C-DE.8344.A.00294/24	12.00
9	10000008	Линолеум бытовой	Экономичный вариант для частных домов	Выберите файл...	6.50	150	600	1	13.00	13.50	145862	RU C-DE.1234.A.01538/24	10.00
10	LAM-VA-01	Ламинат водостойкий	Высокая водостойкость, устойчивость к влажности, надежное покрытие для влажных помещений	Выберите файл...	12.00	130	20	2	8.00	9.00	114862	RU C-DE.1234.A.15964/24	15.00
\.


--
-- Data for Name: products_sales; Type: TABLE DATA; Schema: app; Owner: app
--

COPY app.products_sales (id, partner_id, production_id, count, date, total_price) FROM stdin;
73	1	1	45678.00	2026-04-02	1036890.60
74	2	3	78901.00	2026-04-09	2367030.00
75	3	6	34567.00	2026-04-04	553072.00
76	5	10	95000.00	2026-04-03	1425000.00
77	4	5	1500.00	2026-03-31	27000.00
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

SELECT pg_catalog.setval('app.products_sales_id_seq', 78, true);


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

\unrestrict ZJ6rDljrsz4zpRIj7vvWMAVrQTdYnc0OqrxQeAFRfLLFTatJITQmtLypFZhRYGL

