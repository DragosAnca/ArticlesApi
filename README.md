# REST ArticlesApi

## Tech Stack
- .NET 8
- MongoDB
- Swagger/Postman for testing

## Design Patterns
- **Dependency Injection (& Inversion of Control)**: Used for injecting repositories.
- **Repository Pattern**: Used in separating the Data Access layer from business logic.
- **Singleton Pattern**: Minimize resource usage by instantiating an object only once and reusing it multiple times.
- **In-Memory Caching**: Minimize the calls to the database.
- **Rate limiter**: Lower the load from the endpoints, prevents abuse and ensures fair use.
- **API Versioning**

## Development

### Versioning
- **Code Versioning**: GitHub is used.
- **Feature Branches**: 
  - For each new feature: `ArticlesApi_X_"Title-of-New-feature"`
- **Bugfix Branches**:
  - For each bugfix: `ArticlesApiBugfix-YX_"Title-of-bugfix"`
  - `X` = Issue number
  - `Y` = Release version
    
# Endpoints

## /api/vX/articles
- Accepts **GET** & **POST**
- `X` = API version

## /api/vX/articles/{id}
- Accepts **GET**, **PUT** & **DELETE**

### GET
Due to pagination implementation, `pageNumber` and `pageSize` are optional, but recommended:

```
https://localhost:7288/api/v1/articles?pageNumber=1&pageSize=3
```

Response:  
![image](https://github.com/DragosAnca/ArticlesApi/assets/83972478/4bd2d1e5-a7a0-4d95-bb5c-c24b6c19ca19)

### POST
Adds an article to the database.

**Body:**
```json
{
  "title": "Cafea",
  "content": "Macinata"
}
```

Error responses after validation:  
![image](https://github.com/DragosAnca/ArticlesApi/assets/83972478/df3b473a-56f7-433f-9cc7-9a9d0f75c7eb)  
![image](https://github.com/DragosAnca/ArticlesApi/assets/83972478/0616e66b-8c82-4172-8945-60ef3ee903ef)

## Domain Models

### Article
```json
{
  "id": "string($uuid)",
  "title": "string",
  "content": "string",
  "publishDate": "string($date-time)"
}
```
- **title**: string, required
  - maxLength: 50
  - minLength: 2
- **content**: string, required
  - maxLength: 1000
  - minLength: 10



