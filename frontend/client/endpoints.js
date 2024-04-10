async function buyProduct(name, costBasis) {
    const response = await fetch('/buyProduct', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: name,
            cost_basis: costBasis
        })
    });
    if (!response.ok) {
        throw new Error(`Failed to call BuyProduct: ${response.statusText}`);
    }
}

async function sellProduct(name, salePrice) {
    const response = await fetch('/sellProduct', {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            name: name,
            sale_price: salePrice
        })
    });
    if (!response.ok) {
        throw new Error(`Failed to call SellProduct: ${response.statusText}`);
    }
}

async function getStats(before, after) {
    const response = await fetch(`/getStats?before=${before}&after=${after}`);
    if (!response.ok) {
        throw new Error(`Failed to call GetStats: ${response.statusText}`);
    }
    return await response.json();
}

/* Examples
    buyProduct('Product1', 10.5)
        .then(() => console.log('BuyProduct successful'))
        .catch(error => console.error('BuyProduct failed:', error));

    sellProduct('Product1', 15.5)
        .then(() => console.log('SellProduct successful'))
        .catch(error => console.error('SellProduct failed:', error));

    getStats('2024-01-01T00:00:00Z', '2024-12-31T23:59:59Z')
        .then(() => console.log('GetStats successful'))
        .catch(error => console.error('GetStats failed:', error)); 
*/
