interface UserDetails {
  name: string;
  email: string;
  isAdmin: number;
}

let authToken: string | null = null;

async function loginUser() {
  const credentials = {
    email: "admin@gmail.com",
    password: "admin@123",
  };

  const response = await fetch("http://localhost:5125/User/Login", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(credentials),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  const data = await response.text();
  console.log(data);
  return data;
}

async function fetchUserDetails(token: string) {
  const response = await fetch("http://localhost:5125/User/GetUsers", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  let users: UserDetails[] = await response.json();

  console.log(response.status);
  console.log(users);
}


async function addUser(token: string) {
  const newUser: UserDetails = {
    name: "Rock",
    email: "rock@gmail.com",
    isAdmin: 0,
  };

  const response = await fetch("http://localhost:5125/User/AddUser", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(newUser),
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  const result = await response.json();
  console.log("Book added:", result);

  return result;
}

async function deleteUser(token: string, userId: number) {
  const response = await fetch(
    `http://localhost:5125/User/DeleteUser/${userId}`,
    {
      method: "DELETE",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`,
      },
    }
  );

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  console.log(`User with ID ${userId} deleted successfully`);
}

let token = await loginUser();

await deleteUser(token, 4);

let updatedUsers: UserDetails[] = await(async () => {
  const response = await fetch("http://localhost:5125/User/GetUsers", {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
  });

  if (!response.ok) {
    throw new Error(`HTTP error! status: ${response.status}`);
  }

  return await response.json();
})();

console.log("\n Updated User List:");
updatedUsers.forEach((user) => {
  console.log(
    `User: ${user.name} | Email: ${user.email} | Admin: ${user.isAdmin}`
  );
});