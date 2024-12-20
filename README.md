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

### GET /api/meals/{mealId}

Fetches details for a specific meal.

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
POST http://localhost:5000/api/meals
```

### Response

```http
Status 201
{
    "id": 32,
    "name": "Demo",
    "description": "All the best variants of Lasagna recipies from all across the globe",
    "creationDate": "2024-12-12T17:02:44.3073953Z"
}
```

---

### PUT /api/meals/{mealId}

Updates the description of an existing meal.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "description": "very long description"
}
```

### Example Request:

```http
PUT http://localhost:5000/api/meals/32
```

### Response

```http
Status 200
{
    "id": 32,
    "name": "Demo",
    "description": "very long description",
    "creationDate": "2024-12-12T17:02:44.307395Z"
}
```

---

### DELETE /api/meals/{mealId}

Deletes a specific meal.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5000/api/meals/32
```

### Response

```http
Status 204
```

---

## Recipies

### GET /api/meals/{mealid}/recipies

Fetches a list of recipies.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5000/api/meals/22/recipies
```

### Response

```http
Status 200
[
    {
        "id": 9,
        "name": "Cheese pizza",
        "description": "The cheese pizza features a perfectly round, golden-brown crust, with a soft and chewy center that invites you to take a bite. The surface is generously covered with a thick, gooey layer of melted mozzarella cheese, which stretches with every slice pulled away. The cheese is evenly melted, with a slight golden hue on the edges where it has crisped up, adding texture to every bite. Its simple elegance makes the pizza look warm and comforting, with no toppings to distract from the pure, creamy cheese. The overall appearance is clean, smooth, and inviting, with a timeless appeal that promises a delightful, satisfying experience.",
        "creationDate": "2024-12-08T14:03:05.855679Z"
    },
    {
        "id": 10,
        "name": "Mushroom pizza",
        "description": "The mushroom pizza has a golden-brown, slightly crispy crust forming the outline of a cheerful face. The sauce, spread evenly across the pizza, gives the face a warm, inviting glow, while a layer of melted mozzarella cheese serves as the skin, bubbling and slightly browned in places for added character. Mushrooms are scattered across the pizza, resembling eyes, a nose, and even a quirky smile. A few slivers of red bell pepper or olives add a touch of detail for eyebrows, giving the face a playful, expressive look. The overall expression is one of joy, with a cheesy smile that’s sure to delight anyone who takes a bite.",
        "creationDate": "2024-12-08T14:03:13.608262Z"
    },
    {
        "id": 11,
        "name": "Vegan pizza",
        "description": "The vegan pizza boasts a golden-brown, crispy crust that provides the perfect base for a colorful array of plant-based toppings. A layer of creamy, dairy-free cheese stretches across the surface, offering a smooth, melty texture with slight golden-brown spots where it has crisped up. Vibrant vegetables such as red peppers, onions, mushrooms, spinach, and cherry tomatoes are scattered generously across the pizza, adding a fresh, natural burst of color and flavor. Each bite is a balance of savory, earthy, and slightly sweet flavors, with the toppings offering a satisfying contrast to the soft, slightly chewy crust. The pizza looks fresh, inviting, and full of wholesome goodness, with an appetizing combination of textures and colors that makes it both visually appealing and deliciously vegan.",
        "creationDate": "2024-12-08T14:04:04.874429Z"
    },
    {
        "id": 12,
        "name": "Pineapple pizza",
        "description": "The pineapple pizza features a golden, slightly crispy crust that provides a perfect foundation for its unique combination of toppings. A layer of melted mozzarella cheese blankets the surface, with pockets of gooey cheese adding a satisfying stretch. The star of the pizza, juicy pineapple chunks, are scattered evenly across the pie, their vibrant yellow color contrasting beautifully with the rich cheese and the light golden crust. The pineapple’s sweetness contrasts perfectly with the savory, slightly salty flavor of the cheese, creating a balanced and intriguing flavor profile. The edges of the crust are lightly browned, giving the pizza a rustic, inviting appearance. Each bite offers a delicious mix of sweet and savory, with a perfect balance of textures, from the soft pineapple to the crispy crust and melted cheese.",
        "creationDate": "2024-12-08T14:04:34.126975Z"
    }
]
```

---

### GET /api/meals/{mealid}/recipies/{recipid}

Fetches details for a specific recipie.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5000/api/meals/22/recipies/9
```

### Response

```http
Status 200
{
    "id": 9,
    "name": "Cheese pizza",
    "description": "The cheese pizza features a perfectly round, golden-brown crust, with a soft and chewy center that invites you to take a bite. The surface is generously covered with a thick, gooey layer of melted mozzarella cheese, which stretches with every slice pulled away. The cheese is evenly melted, with a slight golden hue on the edges where it has crisped up, adding texture to every bite. Its simple elegance makes the pizza look warm and comforting, with no toppings to distract from the pure, creamy cheese. The overall appearance is clean, smooth, and inviting, with a timeless appeal that promises a delightful, satisfying experience.",
    "creationDate": "2024-12-08T14:03:05.855679Z"
}
```

---

### POST  /api/meals/{mealid}/recipies

Creates a new recipie.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "name": "Cheesy lasagna",
    "description": "Cheesy lasagna, as the name suggests, is a variation that places extra emphasis on the cheese. While it still features the classic layers of pasta and savory meat sauce, this version is topped with an abundance of melted mozzarella and parmesan cheeses, creating a gooey, cheesy top layer. The cheese is often layered throughout the dish, making each bite extra indulgent and cheesy. The result is a lasagna that is even more decadent, with the cheese creating a stretchy, satisfying texture that complements the hearty sauce and pasta. It’s a dish that will please any cheese lover with its rich, creamy, and melt-in-your-mouth goodness."
}
```

### Example Request:

```http
POST http://localhost:5000/api/meals/24/recipies
```

### Response

```http
Status 201
{
    "id": 23,
    "name": "Cheesy lasagna",
    "description": "Cheesy lasagna, as the name suggests, is a variation that places extra emphasis on the cheese. While it still features the classic layers of pasta and savory meat sauce, this version is topped with an abundance of melted mozzarella and parmesan cheeses, creating a gooey, cheesy top layer. The cheese is often layered throughout the dish, making each bite extra indulgent and cheesy. The result is a lasagna that is even more decadent, with the cheese creating a stretchy, satisfying texture that complements the hearty sauce and pasta. It’s a dish that will please any cheese lover with its rich, creamy, and melt-in-your-mouth goodness.",
    "creationDate": "2024-12-12T17:18:18.8232309Z"
}
```

---

### PUT  /api/meals/{mealid}/recipies/{recipid}

Updates the description of an existing recipie.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "description": "very long description"
}
```

### Example Request:

```http
PUT http://localhost:5000/api/meals/24/recipies/23
```

### Response

```http
Status 200
{
    "id": 23,
    "name": "Cheesy lasagna",
    "description": "very long description",
    "creationDate": "2024-12-12T17:18:18.82323Z"
}
```

---

### DELETE  /api/meals/{mealid}/recipies/{recipid}

Deletes a specific recipie.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5000/api/meals/32
```

### Response

```http
Status 204
```

---

## Comments

### GET /api/meals/{mealid}/recipies/{recipieid}/comments

Fetches a list of comments.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5000/api/meals/22/recipies/9/comments
```

### Response

```http
Status 200
[
    {
        "id": 20,
        "content": "LABAI GERAS RECEPTAS, 10/10",
        "creationDate": "2024-12-12T16:14:15.146574Z"
    },
    {
        "id": 21,
        "content": "O MAN NELABAI PATIKO, FUJ",
        "creationDate": "2024-12-12T16:14:22.318522Z"
    }
]
```

---

### GET /api/meals/{mealid}/recipies/{recipieid}/comments/{commentid}

Fetches details for a specific comment.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | No        |

### Example Request:

```http
GET http://localhost:5000/api/meals/22/recipies/9/comments/20
```

### Response

```http
Status 200
{
    "id": 20,
    "content": "LABAI GERAS RECEPTAS, 10/10",
    "creationDate": "2024-12-12T16:14:15.146574Z"
}
```

---

### POST  /api/meals/{mealid}/recipies/{recipieid}/comments

Creates a new comment.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "content": "Fuj neskanu"
}
```

### Example Request:

```http
POST http://localhost:5000/api/meals/22/recipies/9/comments
```

### Response

```http
Status 201
{
    "id": 22,
    "content": "Fuj neskanu",
    "creationDate": "2024-12-12T17:27:01.5560578Z"
}
```

---

### PUT  /api/meals/{mealid}/recipies/{recipieid}/comments/{commentid}

Updates the description of an existing comment.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Body:

```http
{
    "content": "MAN LABAI LABAI PATIKO"
}
```

### Example Request:

```http
PUT http://localhost:5000/api/meals/22/recipies/9/comments/22
```

### Response

```http
Status 200
{
    "id": 22,
    "content": "MAN LABAI LABAI PATIKO",
    "creationDate": "2024-12-12T17:27:01.556057Z"
}
```

---

### DELETE  /api/meals/{mealid}/recipies/{recipieid}/comments/{commentid}

Deletes a specific comment.

### Resource Information:

| Property                | Value     |
|-------------------------|-----------|
| Response format         | JSON      |
| Requires authentication | Yes       |

### Example Request:

```http
DELETE http://localhost:5000/api/meals/22/recipies/9/comments/22
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
    "userName": "aukse123",
    "Email": "aukse@email.com",
    "Password": "StrongPassword1!"
}
```

### Example Request:
```http
POST http://localhost:5000/api/register
```

### Response

```http
Status 201
{
    "userId": "643abfe3-b070-4539-9b88-a709a8219e6d",
    "userName": "aukse123",
    "email": "aukse@email.com"
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
    "userName": "aukse1",
    "Password": "StrongPassword1!"
}
```

### Example Request:
```http
POST http://localhost:5000/api/login
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
POST http://localhost:5000/api/logout
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
5. Kliento ir serverio dalys yra pasiekiamos per debesis.
6. Pateikta detali ataskaita.
