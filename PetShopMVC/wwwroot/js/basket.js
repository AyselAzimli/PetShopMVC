

    function addToWishList(id) {
        fetch(`https://localhost:7020/wishList/addToWishList/${id}`)
            .then(response => response.json())
            .then(data => { });
    }

    function removeFromWishList(id) {
        fetch(`https://localhost:7020/wishList/removeFromWishList/${id}`)
            .then(response => response.json())
            .then(data => {});
    }
