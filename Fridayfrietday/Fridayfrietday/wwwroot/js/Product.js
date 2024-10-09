function openCustomizeModal(productId, productName) {
    document.getElementById('modalProductId').value = productId;
    document.getElementById('modalProductName').textContent = productName; // Update product name in modal

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
            const productName = button.getAttribute('data-product-name'); // Get the product name

            // Set the hidden field with the productId
            document.getElementById('modalProductId').value = productId;

            // Set the modal title with the product name
            document.getElementById('customiseModalLabel').textContent = "Customize " + productName;

            // Show the customize modal
            var customiseModal = new bootstrap.Modal(document.getElementById('customiseModal'));
            customiseModal.show();
        });
    });
});


document.addEventListener('DOMContentLoaded', function () {
    // Add click event to each 'Add to Cart' button
    const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
    addToCartButtons.forEach(button => {
        button.addEventListener('click', function () {
            const productId = button.getAttribute('data-product-id');
            const form = button.closest('form'); // Find the closest form element related to this button

            if (!form) {
                console.error('Form not found for product ID:', productId);
                return;
            }

            const formData = new FormData(form);

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
                    // Handle success response here
                    alert('Product added to cart successfully!');
                    console.log(data);
                })
                .catch(error => {
                    // Handle error response here
                    alert('There was an error adding the product to the cart.');
                    console.error('Error:', error);
                });
        });
    });
});

