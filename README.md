# Multi-Service API Documentation

## Getting Started

### 1. **Command to Start the Docker Containers**

Run the following command to get the Docker containers up and running:

```
docker-compose up --build
```

This command will build and start all services (OrganizationService, UserService, and DocumentService) along with any necessary dependencies like SQL Server.

### 2. **Access Swagger Documentation**

Once the containers are up, you can access the **Swagger UI** for each service by navigating to the following URLs:

- **OrganizationService Swagger Docs**: [https://localhost:5001/swagger](https://localhost:5001/swagger)  
- **UserService Swagger Docs**: [https://localhost:5003/swagger](https://localhost:5003/swagger)  
- **DocumentService Swagger Docs**: [https://localhost:5005/swagger](https://localhost:5005/swagger)

> **Note**: Replace the ports (`5001`, `5003`, `5005`) if you've configured different ports in your `docker-compose.yml` file.

---

## Example Scenario

### 1. **Go to OrganizationService Swagger Docs**

- Navigate to [https://localhost:5001/swagger](https://localhost:5001/swagger) to access the **OrganizationService** API.
  
#### **Create a New Organization**

- From the Swagger UI, go to the **POST /api/organizations** endpoint.  
- Click **Try it out** and fill in the organization details (e.g., name).  
- Click **Execute** to create the organization.  
- **Save the organization ID** from the response, as you'll need it in the next steps.

### 2. **Go to UserService Swagger Docs**

- Navigate to [https://localhost:5003/swagger](https://localhost:5003/swagger) to access the **UserService** API.

#### **Create a New User**

- From the Swagger UI, go to the **POST /api/users** endpoint.  
- Click **Try it out** and fill in the user details (e.g., email).  
- Click **Execute** to create the user.  
- **Save the user ID** from the response for future steps.

#### **Assign the User to the Organization**

- Go to the **POST /api/users/{userId}/assign-organization** endpoint in the UserService Swagger UI.  
- Click **Try it out**, enter the `userId` you got from the previous step and the `organizationId` you saved earlier.  
- Click **Execute** to assign the user to the organization.

### 3. **Go to DocumentService Swagger Docs**

- Navigate to [https://localhost:5005/swagger](https://localhost:5005/swagger) to access the **DocumentService** API.

#### **Add a Document for the User**

- Go to the **POST /api/documents/user/{userId}** endpoint.  
- Click **Try it out**, enter the `userId` (from the previous step), and provide the document details (e.g., name, size, and storage path).  
- Click **Execute** to add the document for the user.

#### **Add a Document for the Organization**

- Go to the **POST /api/documents/organization/{organizationId}** endpoint.  
- Click **Try it out**, enter the `organizationId` (from the first step), and provide the document details (e.g., name, size, and storage path).  
- Click **Execute** to add the document for the organization.

---

## Full Example Flow Summary

1. **Create a new organization** via OrganizationService.  
2. **Create a new user** via UserService.  
3. **Assign the user to the organization** via UserService.  
4. **Add a document for the user** via DocumentService.  
5. **Add a document for the organization** via DocumentService.