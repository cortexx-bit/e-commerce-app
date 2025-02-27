// site.js
// Contact us form submission script
document.addEventListener('DOMContentLoaded', function () {
    const contactForm = document.getElementById('contactForm');
    const successMessage = document.getElementById('successMessage');

    contactForm.addEventListener('submit', function (event) {
        event.preventDefault();
        successMessage.style.display = 'block';
    });
});

// validation to prevent manual input above 20
document.querySelectorAll('.quantity-input').forEach(input => {
    input.addEventListener('change', function () {
        let value = parseInt(this.value);
        if (isNaN(value) || value < 1) {
            this.value = 1;  
        } else if (value > 20) {
            this.value = 20;
        }
        const itemId = this.id.replace('quantity-', '');
        const hiddenInput = document.getElementById(`hidden-quantity-${itemId}`);
        if (hiddenInput) {
            hiddenInput.value = this.value;
        }
    });
});
// Client-side validation to clamp quantity to 1-20 and prevent invalid submissions
document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('checkoutForm');
    const quantityInputs = document.querySelectorAll('.quantity-input');
    const updateButton = document.getElementById('updateButton');

    // Clamp quantity on change and prevent submission with invalid values
    quantityInputs.forEach(input => {
        input.addEventListener('change', function () {
            let value = parseInt(this.value);
            if (isNaN(value) || value < 1) {
                this.value = 1; 
            } else if (value > 20) {
                this.value = 20;
            }
            console.log(`Quantity for item ${this.name} clamped to ${this.value}`);
        });

        // Prevent Enter key from submitting the form unless quantity is valid
        input.addEventListener('keypress', function (event) {
            if (event.key === 'Enter') {
                event.preventDefault(); 
                const value = parseInt(this.value);
                if (isNaN(value) || value < 1 || value > 20) {
                    this.value = Math.clamp(value, 1, 20) || 1; 
                    alert('Quantity must be between 1 and 20. It has been adjusted.');
                }
                updateButton.click(); 
            }
        });
    });

    // Ensure form submission only proceeds with valid quantities
    form.addEventListener('submit', function (event) {
        let hasInvalidQuantity = false;
        quantityInputs.forEach(input => {
            const value = parseInt(input.value);
            if (isNaN(value) || value < 1 || value > 20) {
                hasInvalidQuantity = true;
                input.value = Math.clamp(value, 1, 20) || 1; 
            }
        });

        if (hasInvalidQuantity) {
            event.preventDefault(); 
            alert('Some quantities were outside the allowed range (1-20) and have been adjusted. Please review and submit again.');
            updateButton.click();
        }
    });
});



// Google maps scripts
document.addEventListener('DOMContentLoaded', async () => {
    await customElements.whenDefined('gmpx-store-locator');
    const locator = document.querySelector('gmpx-store-locator');
    locator.configureFromQuickBuilder({
        "locations": [
            {
                "title": "Pepper Street",
                "address1": "Pepper St",
                "address2": "Chester CH1, UK",
                "coords": { "lat": 53.18890762744421, "lng": -2.8895753343933084 },
                "placeId": "EhpQZXBwZXIgU3QsIENoZXN0ZXIgQ0gxLCBVSyIuKiwKFAoSCWfFnFBJ3XpIEdUIc9STfLkCEhQKEgnHQ9MmsMJ6SBEkzZSVlNSQGA"
            }
        ],
        "mapOptions": {
            "center": { "lat": 53.18890762744421, "lng": -2.8895753343933084 },
            "fullscreenControl": true,
            "mapTypeControl": false,
            "streetViewControl": false,
            "zoom": 15,
            "zoomControl": true,
            "maxZoom": 17,
            "mapId": ""
        },
        "mapsApiKey": "AIzaSyCfDBYmYuUvvGnWtbRS9nO6jdavC-us1rY",
        "capabilities": {
            "input": false,
            "autocomplete": false,
            "directions": false,
            "distanceMatrix": false,
            "details": false,
            "actions": false
        }
    });
});
