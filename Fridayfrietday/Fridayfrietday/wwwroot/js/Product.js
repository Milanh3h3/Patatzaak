function openCustomizeModal(productId) {
    document.getElementById('modalProductId').value = productId;
    var customiseModal = new bootstrap.Modal(document.getElementById('customiseModal'));
    customiseModal.show();
}
function customizeAndAddToCart() {
    const productId = document.getElementById('modalProductId').value;

    // Gather selected sauces from checkboxes
    const selectedSauceIds = Array.from(document.querySelectorAll('input[name="SelectedSauces"]:checked')).map(input => input.value);

    const quantity = document.getElementById('quantity').value;

    const formData = new FormData();
    formData.append('ProductId', productId);
    formData.append('SelectedSauces', JSON.stringify(selectedSauceIds)); // Send as JSON string
    formData.append('Quantity', quantity);

    fetch('/Cart/AddToCart', {
        method: 'POST',
        body: formData,
        headers: {
            'X-Requested-With': 'XMLHttpRequest'
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            throw new Error('Network response was not ok.');
        })
        .then(data => {
            alert('Customized product added to cart successfully!');
            console.log(data);
            // Close the modal
            const customiseModal = bootstrap.Modal.getInstance(document.getElementById('customiseModal'));
            customiseModal.hide();
        })
        .catch(error => {
            alert('There was an error adding the customized product to the cart.');
            console.error('Error:', error);
        });
}

document.addEventListener('DOMContentLoaded', function () {
    // Add click event to Customize buttons
    const customizeButtons = document.querySelectorAll('.customize-btn');
    customizeButtons.forEach(button => {
        button.addEventListener('click', function (event) {
            event.preventDefault();
            const productId = button.getAttribute('data-product-id');
            document.getElementById('modalProductId').value = productId;

            // Show the customize modal
            var customiseModal = new bootstrap.Modal(document.getElementById('customiseModal'));
            customiseModal.show();
        });
    });
});
function addToCart(productId) {
    const formData = new FormData(document.getElementById('addToCartForm'));
    formData.append('productId', productId); // Add the product ID to the form data

    fetch('/Cart/AddToCart', {
        method: 'POST',
        body: formData,
        headers: {
            'X-Requested-With': 'XMLHttpRequest' // Indicates that this is an AJAX request
        }
    })
        .then(response => {
            if (response.ok) {
                return response.json(); // Expect a JSON response from the server
            }
            throw new Error('Network response was not ok.');
        })
        .then(data => {
            // Handle success response here
            alert('Product added to cart successfully!');
            console.log(data); // You can log the response data for debugging
        })
        .catch(error => {
            // Handle error response here
            alert('There was an error adding the product to the cart.');
            console.error('Error:', error);
        });
}