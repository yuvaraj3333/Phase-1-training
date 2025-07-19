const defaultProducts = [
  {
    id: 1,
    name: "Margherita Pizza",
    category: "Pizza",
    price: 10.99,
    launchDate: "2023-01-01",
    freeDelivery: true,
    image: "https://media.gettyimages.com/id/1342044533/photo/margherita-pizza.jpg?s=612x612&w=0&k=20&c=4QF9xXt_9W1xHx0pWqkKnOSbKXe2nhknHUm2sOxjS4o="
  },
  {
    id: 2,
    name: "Veggie Burger",
    category: "Burger",
    price: 7.99,
    launchDate: "2023-05-15",
    freeDelivery: false,
    image: "https://upload.wikimedia.org/wikipedia/commons/6/60/Veggie_Burger.jpg"
  },
  {
    id: 3,
    name: "Caesar Salad",
    category: "Salad",
    price: 5.99,
    launchDate: "2023-02-10",
    freeDelivery: true,
    image: "https://www.simplyrecipes.com/thmb/Aqs1AEjJBA7-ZwCCSLXzCn5g58g=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/Simply-Recipes-Caesar-Salad-LEAD-4-bcaa3e13670245d987c1bd9c911e5e20.jpg"
  },
  {
    id: 4,
    name: "Pasta Alfredo",
    category: "Pasta",
    price: 12.5,
    launchDate: "2023-03-05",
    freeDelivery: true,
    image: "https://www.allrecipes.com/thmb/4xJ6d0WSzXxEqfYi6lLPAcH7Mxc=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/2741687-easy-alfredo-sauce-Rita-1x1-1-74097f3d5d9240d786ff0b7e0d1563c2.jpg"
  },
  {
    id: 5,
    name: "Sushi Roll",
    category: "Sushi",
    price: 15.0,
    launchDate: "2023-06-10",
    freeDelivery: false,
    image: "https://image.shutterstock.com/image-photo/assorted-sushi-rolls-on-black-260nw-2524703433.jpg"
  },
  {
    id: 6,
    name: "Chicken Wings",
    category: "Wings",
    price: 9.99,
    launchDate: "2023-01-20",
    freeDelivery: true,
    image: "https://static01.nyt.com/images/2021/02/03/dining/03BuffaloWingsrex1/03BuffaloWingsrex1-superJumbo.jpg"
  },
  {
    id: 7,
    name: "Chocolate Cake",
    category: "Dessert",
    price: 6.5,
    launchDate: "2023-04-25",
    freeDelivery: true,
    image: "https://www.biggerbolderbaking.com/wp-content/uploads/2022/06/Chocolate-Lava-Cake-Thumbnail-scaled.jpg"
  },
  {
    id: 8,
    name: "Fruit Juice",
    category: "Beverage",
    price: 3.5,
    launchDate: "2023-01-01",
    freeDelivery: false,
    image: "https://www.healthifyme.com/blog/wp-content/uploads/2022/03/Fruit-Juices-1.jpg"
  },
  {
    id: 9,
    name: "Grilled Sandwich",
    category: "Sandwich",
    price: 5.0,
    launchDate: "2023-02-20",
    freeDelivery: true,
    image: "https://www.indianhealthyrecipes.com/wp-content/uploads/2021/07/grilled-sandwich-recipe.jpg"
  },
  {
    id: 10,
    name: "French Fries",
    category: "Sides",
    price: 3.0,
    launchDate: "2023-01-15",
    freeDelivery: false,
    image: "https://www.cookwithmanali.com/wp-content/uploads/2023/01/Crispy-French-Fries-500x500.jpg"
  }
];

let products = JSON.parse(localStorage.getItem("qfProducts")) || defaultProducts;
localStorage.setItem("qfProducts", JSON.stringify(products));

function saveProducts() {
  localStorage.setItem("qfProducts", JSON.stringify(products));
}

function renderMenu() {
  const menuDiv = document.getElementById("menuItems");
  menuDiv.innerHTML = "";

  products.forEach((product, index) => {
    const col = document.createElement("div");
    col.className = "col-md-4";

    col.innerHTML = `
      <div class="card h-100">
        <img src="${product.image}" class="card-img-top" alt="${product.name}">
        <div class="card-body">
          <h5 class="card-title">${product.name}</h5>
          <p class="card-text">
            Category: ${product.category}<br>
            Launch Date: ${product.launchDate}<br>
            Price: $<span class="price-text">${product.price.toFixed(2)}</span>
          </p>
          <button class="btn btn-warning edit-price-btn">Edit Price</button>
          <div class="edit-price-form mt-2" style="display: none;">
            <input type="number" step="0.01" class="form-control edit-input" value="${product.price}">
            <button class="btn btn-success btn-sm save-price-btn">Save</button>
            <button class="btn btn-secondary btn-sm cancel-price-btn">Cancel</button>
          </div>
        </div>
      </div>
    `;

    menuDiv.appendChild(col);
  });

  document.querySelectorAll(".edit-price-btn").forEach((btn, i) => {
    btn.onclick = () => {
      const card = btn.closest(".card-body");
      card.querySelector(".edit-price-form").style.display = "block";
      btn.style.display = "none";
    };
  });

  document.querySelectorAll(".cancel-price-btn").forEach((btn, i) => {
    btn.onclick = () => {
      const form = btn.closest(".edit-price-form");
      form.style.display = "none";
      form.parentElement.querySelector(".edit-price-btn").style.display = "inline-block";
    };
  });

  document.querySelectorAll(".save-price-btn").forEach((btn, i) => {
    btn.onclick = () => {
      const input = btn.closest(".edit-price-form").querySelector("input");
      let newPrice = parseFloat(input.value);
      if (!isNaN(newPrice) && newPrice >= 0) {
        products[i].price = newPrice;
        saveProducts();
        renderMenu();
      } else {
        alert("Invalid price entered.");
      }
    };
  });
}

function renderOrders() {
  const orders = JSON.parse(localStorage.getItem("qfOrders")) || [];
  const ordersTable = document.getElementById("ordersTable");

  if (orders.length === 0) {
    ordersTable.innerHTML = `<p>No orders found.</p>`;
    return;
  }

  let tableHTML = `
    <table class="table table-bordered table-striped">
      <thead class="table-light">
        <tr>
          <th>Order ID</th>
          <th>Item</th>
          <th>Quantity</th>
          <th>Price</th>
          <th>Total Price</th>
        </tr>
      </thead>
      <tbody>
  `;

  orders.forEach((order, idx) => {
    tableHTML += `
      <tr>
        <td>${order.id || idx + 1}</td>
        <td>${order.name}</td>
        <td>${order.quantity || 1}</td>
        <td>$${order.price.toFixed(2)}</td>
        <td>$${(order.quantity * order.price).toFixed(2)}</td>
      </tr>
    `;
  });

  tableHTML += `</tbody></table>`;
  ordersTable.innerHTML = tableHTML;
}

document.getElementById("logoutLink").addEventListener("click", (e) => {
  e.preventDefault();
  localStorage.removeItem("qfUser");
  window.location.href = "login.html";
});

renderMenu();
renderOrders();
