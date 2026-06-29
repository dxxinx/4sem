"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
function checkStock(item) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const stock = ["пицца", "бургер", "суши"];
            if (stock.includes(item)) {
                resolve({ item: item, price: 600 });
            }
            else {
                reject("Товара нет на складе");
            }
        }, 1000);
    });
}
function processPayment(order) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            const balance = 1000;
            if (balance >= order.price) {
                resolve(order);
            }
            else {
                reject("Недостаточно денег");
            }
        }, 2000);
    });
}
function deliverOrder(order) {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve("Заказ доставлен: " + order.item);
        }, 1500);
    });
}
checkStock("пицца")
    .then(processPayment)
    .then(deliverOrder)
    .then(result => console.log(result))
    .catch(error => console.log("Ошибка:", error))
    .finally(() => {
    console.log("Спасибо за заказ, приходите еще!");
});
// Задание 2. 
function fetchFast() {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve("Быстрый запрос");
        }, 500);
    });
}
function fetchSlow() {
    return new Promise(resolve => {
        setTimeout(() => {
            resolve("Медленный запрос");
        }, 2000);
    });
}
Promise.race([fetchFast(), fetchSlow()])
    .then(result => console.log(result));
// Задание 3. 
const promises = [
    Promise.resolve("Успех 1"),
    Promise.resolve("Успех 2"),
    Promise.reject("Ошибка 1"),
    Promise.resolve("Успех 3"),
    Promise.reject("Ошибка 2")
];
Promise.allSettled(promises)
    .then(results => {
    results.forEach(result => {
        if (result.status === "fulfilled") {
            console.log(result.value);
        }
    });
});
// Задание 4.
console.log('Начало');
setTimeout(() => console.log('Таймаут'), 0);
Promise.resolve()
    .then(() => console.log('Промис 1'))
    .then(() => console.log('Промис 2'));
console.log('Конец');
// Задание 5.
async function getData() {
    try {
        const response = await fetch('https://api.example.com/data');
        if (!response.ok) {
            throw new Error("Ошибка запроса");
        }
        const data = await response.json();
        console.log(data);
    }
    catch (err) {
        console.error("Ошибка:", err);
    }
}
// Задание 6. 
async function limitRequests(urls, limit) {
    for (let i = 0; i < urls.length; i += limit) {
        const batch = urls.slice(i, i + limit);
        const promises = batch.map(url => fetch(url));
        await Promise.all(promises);
        console.log("Загружена пачка:", batch);
    }
}
const urls = [
    "img1.jpg", "img2.jpg", "img3.jpg",
    "img4.jpg", "img5.jpg", "img6.jpg",
    "img7.jpg", "img8.jpg", "img9.jpg", "img10.jpg"
];
limitRequests(urls, 3);
//# sourceMappingURL=app.js.map