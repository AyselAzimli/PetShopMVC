//document.addEventListener("DOMContentLoaded", function () {
//    const basketItemsTotal = document.getElementById("basketItemsTotal");
//    const basketItems = document.getElementById("basketItems");

//    window.addToBasket = function(id, event) {
//    event.preventDefault(); // stop link from navigating
//    fetch(`https://localhost:7020/basket/addtobasket/${id}`)
//        .then(response => response.json())
//        .then(data => buildCart(data));
//}

//window.removeFromBasket = function(id, event) {
//    event.preventDefault();
//    fetch(`https://localhost:7020/basket/removeFromBasket/${id}`)
//        .then(response => response.json())
//        .then(data => buildCart(data));
//}


//    function buildCart(data) {
//        basketItems.innerHTML = "";
//        basketItemsTotal.innerHTML = `$${data.total}`;

//        for (let product of data.items) {
//            basketItems.innerHTML += `
//                <li class="list-group-item d-flex justify-content-between lh-sm">
//                    <div>
//                        <h6 class="my-0">${product.name}</h6>
//                        <small class="text-body-secondary">Brief description</small>
//                    </div>
//                    <span class="text-body-secondary">$${product.price}</span>
//                    <a href="#" class="product-close">
//                        <i class="fal fa-times" onclick="removeFromBasket(${product.id})"></i>
//                    </a>
//                </li>`;
//        }
//    }

//    function initBasket() {
//        fetch(`https://localhost:7020/basket/initBasket`)
//            .then(response => response.json())
//            .then(data => buildCart(data));
//    }

//});
