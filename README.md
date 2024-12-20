# T120B165_module_project
# Rentora

# 1.	Projekto aprašymas
Kuriamas projektas – minimali nuomojamu kambario sistema (House -> Room -> Note). 

## 1.1. Sistemos paskirtis

    Sistema yra skirta padėti palengvinti nuomotojų ir nuomininkų darbą bei pagerinti priimamų sprendimų kokybę, teikiant nuomos ir naudojantis nuomos pąslaugomis.
    Vartotojai šioje sistemoje galės:
    1. Prisiregistruoti prie platformos.
    2. Prisijungti prie platformos.
    3. Peržiūrėti platformos reprezentacinį puslapį.
    4. Peržiūrėti platformos siūlomus nuomos objektus ir su jais susijusią informaciją.
    5. Pateikti nuomos prašymą pasirinktam nuomos objektui/kambariui.
    6. Peržiūrėti savo šiuo metu nuomuojamąsi objektą/kambarį ir su juo susijusią informaciją.
    
    
## 1.2. Funkciniai reikalavimai
    Neregistruotas sistemos naudotojas galės:
    Peržiūrėti platformos reprezentacinį puslapį.
    Prisiregistruoti prie platformos.
    Prisijungti prie platformos.
    
    Registruotas nuomininkas galės:
    Viską, ką ir neregistruotas naudotojas.
    Peržiūrėti platformos siūlomus nuomos objektus ir su jais susijusią informaciją.
    Pateikti užrašus pasirinktam nuomos objektui/kambariui.
    
    Administratorius galės:
    Viską, ką ir neregistruotas naudotojas.
    Šalinti nuomos objektą/objekto kambarį ir prideti.
    Kurti kambarius bei juos redaguoti ir trinti.
    Kurti užrašus bei juos redaguoti ir trinti.

## 2. Pasirinktų technologijų aprašymas
Projekto ‚Frontend‘ bus kuriamas su React technologija. Duomenų bazė – PostgreSQL. Serverio pusė - naudojant .Net Core Web API
Sistemos architektūra:
![image](https://github.com/user-attachments/assets/148becc0-60f8-4b67-83a4-17649c67d24c)


   # 3. 	Naudotojo sąsaja
## 3.1 Pagrindinis/Home puslaptis
### Wireframe
![image](https://github.com/user-attachments/assets/d5c90305-7007-4e56-9feb-b57cffd5ffe6)



### Realus pavyzdys
![image](https://github.com/user-attachments/assets/f37ad168-a85f-4a77-aa5c-91c25b5a538e)


## 3.2 Atitinkamo house puslaptis
### Wireframe
![image](https://github.com/user-attachments/assets/eb43c9a8-14de-47b0-8f4d-6c9f342ab5da)



### Realus pavyzdys
![image](https://github.com/user-attachments/assets/9a7f60fa-221f-45bd-b881-acf7bfb3cba8)



## 3.3 Note
### Wireframe
![image](https://github.com/user-attachments/assets/cb960b83-3dd1-4548-b450-97578ffdd548)



### Realus pavyzdys
![image](https://github.com/user-attachments/assets/58bc002b-baae-4c4c-9341-96c0890d2896)



### 3.4 House kurimo forma
### Wireframe
![image](https://github.com/user-attachments/assets/25b642ef-e8df-4206-b0bf-1e68611531e4)


### Realus pavyzdys 
![image](https://github.com/user-attachments/assets/48a1a76a-69c4-45f9-8797-d6165f4f835a)


### 3.5 Room kurimo forma
### Wireframe
![image](https://github.com/user-attachments/assets/44eec996-42e9-47ae-9614-0f77cc945060)


### Realus pavyzdys 
![image](https://github.com/user-attachments/assets/c0a7e0b5-7d57-487f-8ef6-131315fe392e)


## 3.6 Registracijos forma
### Wireframe
![image](https://github.com/user-attachments/assets/ba927e83-8a39-48ee-a011-25309b3c71b9)


### Realus pavyzdys 
![image](https://github.com/user-attachments/assets/9f065ab2-c411-4926-8863-3cca2535ec47)

# 4.	API specifikacija

## Houses

### GET /api/houses

Fetches a list of houses.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses
```

### Response

```http
Status 200
[
    
        "id": 1,
        "createdAt": "2024-12-19T21:02:13.347931+00:00",
        "name": "Viezbutis",
        "region": "Kaunas",
        "district": "Panemune"
    
]
```

---

### GET /api/houses/{houseId}

Fetches details for a specific house.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses/1
```

### Response

```http
Status 200
 {
        "id": 1,
        "createdAt": "2024-12-19T21:02:13.347931+00:00",
        "name": "Viezbutis",
        "region": "Kaunas",
        "district": "Panemune"
    },
```

---

### POST /api/houses

Creates a new House.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
Status 200
{
    "name": "null",
    "region": "nullnull",
    "district": "nullnull"
}
```

### Example Request:

```http
POST http://localhost:5106/api/houses
```

### Response

```http
Status 201
 {
        "id": 1,
        "createdAt": "2024-12-19T21:02:13.347931+00:00",
        "name": "Viezbutis",
        "region": "Kaunas",
        "district": "Panemune"
    },
```

---

### PUT /api/houses/{houseId}

Updates the name, region, dictricct of an existing house.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    {
    "name": "gsdgsdgsdgsdg",
    "region": "nullnull",
    "district": "fsssse"
    
}
}
```

### Example Request:

```http
PUT http://localhost:5106/api/houses/1
```

### Response

```http
Status 200
{
     {
        "id": 1,
        "createdAt": "2024-12-19T21:02:13.347931+00:00",
        "name": "Viezbutis",
        "region": "Kaunas",
        "district": "Panemune"
    },
}
```

---

### DELETE /api/houses/{houseId}

Deletes a specific meal.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5106/api/houses/32
```

### Response

```http
Status 204
```

---

## rooms

### GET /api/houses/{houseId}/rooms

Fetches a list of rooms.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses/1/rooms
```

### Response

```http
Status 200
[
     {
        "id": 1,
        "number": 1,
        "description": "Su langu i Nemuna",
        "price": 50
    },
    {
        "id": 2,
        "number": 2,
        "description": "Sutoktiniams",
        "price": 250
    }
]
```

---

### GET /api/houses/{houseId}/rooms/{roomId}

Fetches details for a specific room.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses/1/rooms/1
```

### Response

```http
Status 200
 {
        "id": 1,
        "number": 1,
        "description": "Su langu i Nemuna",
        "price": 50
    },
```

---

### POST  /api/houses/{houseId}/rooms

Creates a new room.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "number": 5,
    "description": "Su",
    "price": 4
}
```

### Example Request:

```http
POST http://localhost:5106/api/houses/1/rooms
```

### Response

```http
Status 201
{
    "number": 5,
    "description": "Su",
    "price": 4
}
```

---

### PUT  /api/houses/{houseId}/rooms/{roomId}

Updates the description of an existing room.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "number": 99999,
    "description": "rrr",
    "price": 99
}
```

### Example Request:

```http
PUT http://localhost:5106/api/houses/1/rooms/1
```

### Response

```http
Status 200
{
    "number": 99999,
    "description": "rrr",
    "price": 99
}
```

---

### DELETE  /api/houses/{houseId}/rooms/{roomId}

Deletes a specific room.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5106/api/houses/1
```

### Response

```http
Status 204
```

---

## note

### GET /api/houses/{houseId}/rooms/{roomId}/note

Fetches a list of note.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses/1/rooms/1/note
```

### Response

```http
Status 200
[
    {
        "id": 2,
        "note": "Sviesus"
    },
    {
        "id": 3,
        "note": "Siltas"
    },
    {
        "id": 4,
        "note": "Geras kambariukas"
    }
]
]
```

---

### GET /api/houses/{houseId}/rooms/{roomId}/note/{noteId}

Fetches details for a specific note.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5106/api/houses/1/rooms/1/note/2
```

### Response

```http
Status 200
  {
        "id": 2,
        "note": "Sviesus"
    },
```

---

### POST  /api/houses/{houseId}/rooms/{roomId}/note

Creates a new note.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "note": "Senas"
    
}
```

### Example Request:

```http
POST http://localhost:5106/api/houses/1/rooms/1/note
```

### Response

```http
Status 201
{
    "note": "Senas"
    
}
```

---

### PUT  /api/houses/{houseId}/rooms/{roomId}/note/{noteId}

Updates the description of an existing note.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "note": "Senas"
    
}
```

### Example Request:

```http
PUT http://localhost:5106/api/houses/1/rooms/1/note/1
```

### Response

```http
Status 200
{
    "id": 5,
    "note": "Senas"
}
```

---

### DELETE  /api/houses/{houseId}/rooms/{roomId}/note/{noteId}

Deletes a specific note.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5106/api/houses/1/rooms/1/note/1
```

### Response

```http
Status 204
```

---

## Autorizacija

### POST api/register

Registers new user

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Body
```http
{
    "userName": "lukas3",
    "email": "lukas3@gmail.com",
    "password": "VerySafePassword1!"
    
}
```

### Example Request:
```http
POST http://localhost:5106/api/register
```

### Response

```http
Status 201
{
   1
}
```

---

### POST api/login

Registers new user

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Body
```http
{
    "userName": "lukas3",
    "Password": "VerySafePassword1!"
}
```

### Example Request:
```http
POST http://localhost:5106/api/login
```

### Response

```http
Status 200
{
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYXVrc2UxIiwianRpIjoiZDg2NWE1ZjAtNzliNy00YjZkLTk0NjQtNmYzYjc5ZTgxOTIwIiwic3ViIjoiN2NlYzVjN2UtMzI3OS00ODFjLWE0M2EtODhmMzJlOTdmOGNiIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiRm9ydW1Vc2VyIiwiZXhwIjoxNzM0MDI1NDY3LCJpc3MiOiJBdWtzZSIsImF1ZCI6IlRydXN0ZWRDbGllbnQifQ.T8vE7tM8A-faKhau1wJ_yW2LCyuLTfwBDOwrS6WFKGk",
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiIxYTcwMjIyMi1hY2QzLTQ1Y2QtODBmNC04YTY3NmVlODlmN2EiLCJzdWIiOiI3Y2VjNWM3ZS0zMjc5LTQ4MWMtYTQzYS04OGYzMmU5N2Y4Y2IiLCJleHAiOjE3MzQxMTEyNjcsImlzcyI6IkF1a3NlIiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.5HJYC7ER7FjwEk8WcDNPW0rSDx-81lM2QRE1MlfrL7E"
}
```

---

### POST api/logout

Registers new user

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Body
```http
{
    "refreshToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJiYjJmNDU4MC03MTYzLTRiZjAtYmIyNS01NDRmN2FmYWRhNjEiLCJzdWIiOiJjZjI2MGM5NC1iMzY0LTQzM2EtYTg0ZS0yNDAxMmM4NThkYWYiLCJleHAiOjE3MzExMDUxMzIsImlzcyI6IkF1a3NlIiwiYXVkIjoiVHJ1c3RlZENsaWVudCJ9.eVWbI3vhN2eE0_lZLerxajw1ja70ltaZgLjyzqr3TeQ"
}
```

### Example Request:
```http
POST http://localhost:5106/api/logout
```

### Response

```http
Status 200
```


# 5. Išvados
1. Sukurtas forum API pasinaudojant REST principais su .NET
2. Duomenų bazei panaudotas PostgresSQL.
3. Klientinei daliai sukurti panaudotas React.js
4. Autorizacijai panaudoti JWT tokenai.
5. Serverio dalys yra pasiekiamos per debesis.
6. Pateikta detali ataskaita.
