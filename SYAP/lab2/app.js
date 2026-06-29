"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var ProdCategory;
(function (ProdCategory) {
    ProdCategory["Food"] = "food";
    ProdCategory["Electronics"] = "electronics";
    ProdCategory["Clothes"] = "clothes";
    ProdCategory["Other"] = "other";
})(ProdCategory || (ProdCategory = {}));
class Product {
    id;
    name;
    price;
    category;
    description;
    constructor(id, name, price, category, description) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.description = description;
    }
    getInfo() {
        return `product id:${this.id}, name product: ${this.name}, price:${this.price}, category: ${this.category}, description: ${this.description || '��� ��������'}`;
    }
}
class Catalog {
    products = [];
    constructor() {
        this.products = [];
    }
    addProd(product) {
        this.products.push(product);
        console.log(`product "${product.name}" added to catalog`);
    }
    removeProd(id) {
        const index = this.products.findIndex(p => p.id == id);
        if (index !== -1) {
            this.products.splice(index, 1);
            console.log(`product with ID ${id} removed`);
            return true;
        }
        else {
            console.log(`product with ID ${id} not found`);
        }
        return false;
    }
    getProdById(id) {
        return this.products.find(p => p.id === id);
    }
    getAllProd() {
        return this.products;
    }
    getProdByCategory(category) {
        return this.products.filter(p => p.category === category);
    }
}
class Order {
    id;
    products;
    totalPrice;
    customerId;
    constructor(id, customerId, products) {
        this.id = id;
        this.products = products;
        this.customerId = customerId;
        this.totalPrice = this.calcTotalPrice();
    }
    calcTotalPrice() {
        return this.products.reduce((sum, product) => sum + product.price, 0);
    }
    getOrderInfo() {
        const productList = this.products.map(p => p.name).join(', ');
        return `order ID: ${this.id}, product: [${productList}], total price: ${this.totalPrice} byn`;
    }
}
class Customer {
    id;
    name;
    email;
    constructor(id, name, email) {
        this.id = id;
        this.name = name;
        this.email = email;
    }
    getCustomerInfo() {
        return `customer ID: ${this.id}, name: ${this.name}, email: ${this.email}`;
    }
}
class OrdersManager {
    orders = [];
    currentOrderId = 1;
    constructor() {
        this.orders = [];
    }
    createOrder(customer, products) {
        const newOrder = new Order(this.currentOrderId++, customer.id, products);
        this.orders.push(newOrder);
        console.log(`order #${newOrder.id} create for ${customer.name}.`);
        return newOrder;
    }
    getOrderById(id) {
        return this.orders.find(order => order.id === id);
    }
    getAllOrders() {
        return this.orders;
    }
    getOrdersByCustomer(customerId) {
        return this.orders.filter(order => order.customerId === customerId);
    }
}
const catalog = new Catalog();
const product1 = new Product(1, "laptop", 3500, ProdCategory.Electronics);
const product2 = new Product(2, "t-shirt", 50, ProdCategory.Clothes);
catalog.addProd(product1);
catalog.addProd(product2);
const customer = new Customer(1, "Aaaa Bbbbb", "ababa@email.com");
const orderManager = new OrdersManager();
const order = orderManager.createOrder(customer, [product1, product2]);
console.log(order.getOrderInfo());
const customerOrders = orderManager.getOrdersByCustomer(customer.id);
console.log(`orders found from the buyer: ${customerOrders.length}`);
//# sourceMappingURL=app.js.map