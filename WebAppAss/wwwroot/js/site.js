// site.js

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