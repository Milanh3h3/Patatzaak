document.addEventListener('DOMContentLoaded', function () {
    // Event listener for quantity buttons
    document.querySelectorAll('.update-quantity').forEach(button => {
        button.addEventListener('click', function () {
            const orderDetailId = this.getAttribute('data-id');
            const change = parseInt(this.getAttribute('data-change'));
            const quantityInput = document.getElementById(`quantity-${orderDetailId}`);

            let currentQuantity = parseInt(quantityInput.value);
            currentQuantity += change;

            // Ensure the quantity does not go below 0
            if (currentQuantity < 0) {
                currentQuantity = 0;
            }

            // Update the input field with the new quantity
            quantityInput.value = currentQuantity;

            // Check if quantity is 0 to remove item
            if (currentQuantity === 0) {
                removeOrderDetail(orderDetailId);
            } else {
                // Send an AJAX request to update the quantity in the backend
                updateOrderDetailQuantity(orderDetailId, currentQuantity);
            }
        });
    });

    // Function to send an AJAX request to update quantity in the backend
    function updateOrderDetailQuantity(orderDetailId, newQuantity) {
        $.ajax({
            url: '/Cart/UpdateQuantity', // Assuming your controller's action is Cart/UpdateQuantity
            type: 'POST',
            data: {
                productId: orderDetailId,
                quantity: newQuantity,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // Anti-forgery token
            },
            success: function (response) {
                location.reload(); // Reload the page to reflect updated totals
            },
            error: function (xhr, status, error) {
                console.error('Error updating quantity:', error);
            }
        });
    }

    // Function to remove an item from the cart
    function removeOrderDetail(orderDetailId) {
        $.ajax({
            url: '/Cart/RemoveItem', // Change this to your actual endpoint for removing items
            type: 'POST',
            data: {
                productId: orderDetailId,
                __RequestVerificationToken: $('input[name="__RequestVerificationToken"]').val() // Anti-forgery token
            },
            success: function (response) {
                // Reload the page or remove the item from the UI directly
                location.reload(); // Reload to reflect changes
            },
            error: function (xhr, status, error) {
                console.error('Error removing item:', error);
            }
        });
    }
});
