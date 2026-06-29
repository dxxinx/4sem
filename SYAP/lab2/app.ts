enum ProdCategory {
    Food = "food",
    Electronics = "electronics",
    Clothes = "clothes",
    Other = "other"
}
class Product {
    public readonly id: number;
    public name: string;
    public price: number;
    public category: ProdCategory;
    public description: string | undefined;
    constructor(id: number, name: string, price: number, category: ProdCategory, description?: string) {
        this.id = id;
        this.name = name;
        this.price = price;
        this.category = category;
        this.description = description;

    }
    public getInfo(): string {
        return `product id:${this.id}, name product: ${this.name}, price:${this.price}, category: ${this.category}, description: ${this.description || 'Нет описания'}`;
    }
}
type PartialProd = Partial<Product>;
type OmitProd = Omit<Product, 'price'>;
type PickProd = Pick<Order<Product>, 'id' | 'totalPrice'>;

class Catalog {
    private products: Product[] = [];
    constructor() {
        this.products = [];
    }
    public addProd(product: Product): void {
        this.products.push(product);
        console.log(`product "${product.name}" added to catalog`);
    }
    public removeProd(id: number): boolean {
        const index = this.products.findIndex(p => p.id == id);
        if (index !== -1) {
            this.products.splice(index, 1);
            console.log(`product with ID ${id} removed`);
            return true;
        } else {
            console.log(`product with ID ${id} not found`);
        }
        return false;
    }
    public getProdById(id: number): Product | undefined {
        return this.products.find(p => p.id === id);
    }
    public getAllProd(): Product[] {
        return this.products;
    }
    public getProdByCategory(category: ProdCategory): Product[] {
        return this.products.filter(p => p.category === category);
    }
}
class Order<T extends Product> {
    public readonly id: number;
    public products: T[];
    public totalPrice: number;
    public customerId: number;

    constructor(id: number, customerId: number, products: T[]) {
        this.id = id;
        this.products = products;
        this.customerId = customerId;
        this.totalPrice = this.calcTotalPrice();
    }
    public calcTotalPrice(): number {
        return this.products.reduce((sum, product) => sum + product.price, 0);
    }
    public getOrderInfo(): string {
        const productList = this.products.map(p => p.name).join(', ');
        return `order ID: ${this.id}, product: [${productList}], total price: ${this.totalPrice} byn`;
    }
}
class Customer {
    public readonly id: number;
    public name: string;
    public email: string;

    constructor(id: number, name: string, email: string) {
        this.id = id;
        this.name = name;
        this.email = email;
    }
    public getCustomerInfo(): string {
        return `customer ID: ${this.id}, name: ${this.name}, email: ${this.email}`;
    }
}
class OrdersManager {
    private orders: Array<Order<Product>> = [];
    private currentOrderId: number = 1;
    constructor() {
        this.orders = [];
    }
    public createOrder(customer: Customer, products: Product[]): Order<Product> {
        const newOrder = new Order<Product>(this.currentOrderId++, customer.id, products);
        this.orders.push(newOrder);
        console.log(`order #${newOrder.id} create for ${customer.name}.`);
        return newOrder;
    }
    public getOrderById(id: number): Order<Product> | undefined {
        return this.orders.find(order => order.id === id);
    }
    public getAllOrders(): Array<Order<Product>> {
        return this.orders;
    }
    public getOrdersByCustomer(customerId: number): Array<Order<Product>> {
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