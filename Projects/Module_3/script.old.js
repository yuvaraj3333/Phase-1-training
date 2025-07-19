
const users = {
  admin: { password: "admin123", role: "admin" },
  john: { password: "john123", role: "customer" },
  emma: { password: "emma123", role: "customer" },
  alex: { password: "alex123", role: "customer" },
};

let defaultMenu = [
  {
    id: 1,
    name: "Margherita Pizza",
    category: "Pizza",
    price: 8.99,
    launchDate: "2023-01-01",
    freeDelivery: true,
    image: "https://source.unsplash.com/400x300/?pizza",
  },
  {
    id: 2,
    name: "Veggie Burger",
    category: "Burger",
    price: 6.5,
    launchDate: "2023-02-15",
    freeDelivery: false,
    image: "https://source.unsplash.com/400x300/?burger",
  },
  {
    id: 3,
    name: "Caesar Salad",
    category: "Salad",
    price: 5.75,
    launchDate: "2023-01-10",
    freeDelivery: true,
    image: "https://source.unsplash.com/400x300/?salad",
  },
  {
    id: 4,
    name: "Spaghetti Bolognese",
    category: "Pasta",
    price: 9.5,
    launchDate: "2023-01-20",
    freeDelivery: false,
    image: "https://source.unsplash.com/400x300/?pasta",
  },
  {
    id: 5,
    name: "Chicken Tikka",
    category: "Indian",
    price: 10.0,
    launchDate: "2023-03-01",
    freeDelivery: true,
    image: "https://source.unsplash.com/400x300/?chicken",
  },
  {
    id: 6,
    name: "Sushi Roll",
    category: "Japanese",
    price: 12.25,
    launchDate: "2023-01-05",
    freeDelivery: false,
    image: "https://source.unsplash.com/400x300/?sushi",
  },
  {
    id: 7,
    name: "Chocolate Cake",
    category: "Dessert",
    price: 4.5,
    launchDate: "2023-01-15",
    freeDelivery: true,
    image: "https://source.unsplash.com/400x300/?dessert",
  },
  {
    id: 8,
    name: "Grilled Sandwich",
    category: "Snacks",
    price: 3.99,
    launchDate: "2023-02-10",
    freeDelivery: false,
    image: "https://source.unsplash.com/400x300/?sandwich",
  },
  {
    id: 9,
    name: "French Fries",
    category: "Snacks",
    price: 2.5,
    launchDate: "2023-01-25",
    freeDelivery: true,
    image: "https://source.unsplash.com/400x300/?fries",
  },
  {
    id: 10,
    name: "Pancakes",
    category: "Breakfast",
    price: 5.0,
    launchDate: "2023-02-20",
    freeDelivery: false,
    image: "https://source.unsplash.com/400x300/?pancakes",
  },
];

const MENU_KEY = "quickfeast_menu";
const CART_KEY_PREFIX = "quickfeast_cart_";
const AUTH_KEY = "quickfeast_auth";


function login(username, password) {
  if (!username || !password) return "Username and password required";

  if (!/^[A-Za-z]+$/.test(username)) return "Username must contain only alphabets";

  if (password.length < 6) return "Password must be minimum 6 characters";

  if (!users[username]) return "Invalid username or password";

  if (users[username].password !== password) return "Invalid username or password";

  // Save auth info
  localStorage.setItem(AUTH_KEY, JSON.stringify({ username, role: users[username].role }));

  return null; 
}

function logout() {
  localStorage.removeItem(AUTH_KEY);
  location.href = "login.html";
}

function getAuth() {
  const auth = localStorage.getItem(AUTH_KEY);
  return auth ? JSON.parse(auth) : null;
}

function checkAuth(role) {
  const auth = getAuth();
  if (!auth) return false;
  if (role && auth.role !== role) return false;
  return true;
}

// --- MENU DATA FUNCTIONS ---

function getMenu() {
  let menu = JSON.parse(localStorage.getItem(MENU_KEY));
  if (!menu) {
    localStorage.setItem(MENU_KEY, JSON.stringify(defaultMenu));
    return defaultMenu;
  }
  return menu;
}

function saveMenu(menu) {
  localStorage.setItem(MENU_KEY, JSON.stringify(menu));
}

// --- RENDER FUNCTIONS ---

function renderMenuForCustomer() {
  const menu = getMenu();
  const container = document.getElementById("menuCards");
  if (!container) return;

  const today = new Date();

  // Only show items active and launched before today
  const filteredMenu = menu.filter((item) => {
    const launch = new Date(item.launchDate);
    return launch <= today;
  });

  container.innerHTML = "";

  filteredMenu.forEach((item) => {
    const card = createMenuCardCustomer(item);
    container.appendChild(card);
  });
}

function renderMenuForAdmin() {
  const menu = getMenu();
  const container = document.getElementById("menuCards");
  if (!container) return;

  container.innerHTML = "";

  menu.forEach((item) => {
    const card = createMenuCardAdmin(item);
    container.appendChild(card);
  });
}

function createMenuCardCustomer(item) {
  const col = document.createElement("div");
  col.className = "col-sm-6 col-md-4 col-lg-3";

  col.innerHTML = `
    <div class="card h-100 shadow-sm">
      <img src="${item.image}" class="card-img-top" alt="${item.name}" />
      <div class="card-body d-flex flex-column">
        <h5 class="card-title">${item.name}</h5>
        <p class="card-text mb-1"><strong>Category:</strong> ${item.category}</p>
        <p class="card-text mb-1"><strong>Launch Date:</strong> ${item.launchDate}</p>
        <p class="card-text mb-1"><strong>Price:</strong> $${item.price.toFixed(2)}</p>
        ${item.freeDelivery ? `<span class="badge bg-success free-delivery-badge mb-2">Free Delivery</span>` : ""}
        <button class="btn btn-primary mt-auto" onclick="addToCart(${item.id})">Add to Cart</button>
      </div>
    </div>
  `;

  return col;
}

function createMenuCardAdmin(item) {
  const col = document.createElement("div");
  col.className = "col-sm-6 col-md-4 col-lg-3";

  col.innerHTML = `
    <div class="card h-100 shadow-sm">
      <img src="${item.image}" class="card-img-top" alt="${item.name}" />
      <div class="card-body">
        <h5 class="card-title">${item.name}</h5>
        <p class="card-text mb-1"><strong>Category:</strong> ${item.category}</p>
        <p class="card-text mb-1"><strong>Launch Date:</strong> ${item.launchDate}</p>
        <p class="card-text mb-1"><strong>Price:</strong> $<span id="price-${item.id}">${item.price.toFixed(2)}</span></p>
        <button class="btn btn-sm btn-warning" onclick="editPrice(${item.id})" id="editBtn-${item.id}">Edit Price</button>
        <div id="editPriceContainer-${item.id}" class="mt-2 d-none">
          <input type="number" min="0" step="0.01" id="priceInput-${item.id}" class="form-control form-control-sm mb-1" value="${item.price.toFixed(2)}" />
          <button class="btn btn-sm btn-success" onclick="savePrice(${item.id})">Save</button>
          <button class="btn btn-sm btn-secondary ms-1" onclick="cancelEdit(${item.id})">Cancel</button>
        </div>
      </div>
    </div>
  `;

  return col;
}


function getCartKey() {
  const auth = getAuth();
  return CART_KEY_PREFIX + (auth ? auth.username : "guest");
}

function getCart() {
  const cartJSON = localStorage.getItem(getCartKey());
  return cartJSON ? JSON.parse(cartJSON) : [];
}

function saveCart(cart) {
  localStorage.setItem(getCartKey(), JSON.stringify(cart));
}

function addToCart(id) {
  const auth = getAuth();
  if (!auth || auth.role !== "customer") {
    alert("Please login as customer to add items to cart.");
    return;
  }

  let cart = getCart();
  const existing = cart.find((item) => item.id === id);
  if (existing) {
    existing.qty++;
  } else {
    cart.push({ id, qty: 1 });
  }
  saveCart(cart);

  alert("Added to cart!");
}

function renderCart() {
  const cart = getCart();
  const menu = getMenu();

  const container = document.getElementById("cartItems");
  const emptyMessage = document.getElementById("emptyMessage");
  const cartTotal = document.getElementById("cartTotal");

  if (!container || !emptyMessage || !cartTotal) return;

  if (cart.length === 0) {
    container.innerHTML = "";
    emptyMessage.classList.remove("d-none");
    cartTotal.textContent = "";
    return;
  }

  emptyMessage.classList.add("d-none");

  container.innerHTML = "";

  let total = 0;

  cart.forEach(({ id, qty }) => {
    const product = menu.find((item) => item.id === id);
    if (!product) return;

    total += product.price * qty;

    const itemElem = document.createElement("div");
    itemElem.className = "list-group-item d-flex align-items-center";

    itemElem.innerHTML = `
      <img src="${product.image}" alt="${product.name}" style="width: 60px; height: 40px; object-fit: cover;" class="me-3 rounded" />
      <div class="flex-grow-1">
        <h6 class="mb-1">${product.name}</h6>
        <small>$${product.price.toFixed(2)} x ${qty} = $${(product.price * qty).toFixed(2)}</small>
      </div>
      <button class="btn btn-sm btn-danger" onclick="removeFromCart(${id})">Remove</button>
    `;

    container.appendChild(itemElem);
  });

  cartTotal.textContent = `Total: $${total.toFixed(2)}`;
}

function removeFromCart(id) {
  let cart = getCart();
  cart = cart.filter((item) => item.id !== id);
  saveCart(cart);
  renderCart();
}

function clearCart() {
  localStorage.removeItem(getCartKey());
  renderCart();
}

function editPrice(id) {
  document.getElementById(`editBtn-${id}`).classList.add("d-none");
  document.getElementById(`editPriceContainer-${id}`).classList.remove("d-none");
}

function cancelEdit(id) {
  document.getElementById(`editBtn-${id}`).classList.remove("d-none");
  document.getElementById(`editPriceContainer-${id}`).classList.add("d-none");
}

function savePrice(id) {
  const input = document.getElementById(`priceInput-${id}`);
  let newPrice = parseFloat(input.value);
  if (isNaN(newPrice) || newPrice < 0) {
    alert("Enter a valid price");
    return;
  }

  let menu = getMenu();
  const itemIndex = menu.findIndex((item) => item.id === id);
  if (itemIndex === -1) return;

  menu[itemIndex].price = newPrice;
  saveMenu(menu);

  document.getElementById(`price-${id}`).textContent = newPrice.toFixed(2);

  cancelEdit(id);

  
  const successMsg = document.getElementById("successMessage");
  if (successMsg) {
    successMsg.textContent = `Price updated for "${menu[itemIndex].name}"`;
    successMsg.classList.remove("d-none");
    setTimeout(() => successMsg.classList.add("d-none"), 3000);
  }
}

if (document.getElementById("loginForm")) {
  document.getElementById("loginForm").addEventListener("submit", (e) => {
    e.preventDefault();

    const username = document.getElementById("username").value.trim();
    const password = document.getElementById("password").value.trim();
    const errorDiv = document.getElementById("loginError");
    errorDiv.textContent = "";

    const error = login(username, password);
    if (error) {
      errorDiv.textContent = error;
    } else {
      // Redirect based on role
      const auth = getAuth();
      if (auth.role === "admin") {
        location.href = "admin.html";
      } else {
        location.href = "customer.html";
      }
    }
  });
}
    
