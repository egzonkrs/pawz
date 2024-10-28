function addToWishlist(petId) {
    console.log("Attempting to add pet with ID:", petId);

    $.ajax({
        url: '/Wishlist/AddPetToWishlist',
        type: 'POST',
        data: { petId: petId },
        success: function (result) {
            console.log("Successfully hit endpoint for pet ID:", petId);
            console.log("Server response:", result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Error hitting endpoint for pet ID:", petId);
            console.error("Status:", textStatus);
            console.error("Error details:", errorThrown);
        }
    });
}

function removeFromWishlist(petId) {
    console.log("Attempting to add pet with ID:", petId);

    $.ajax({
        url: '/Wishlist/RemovePetFromWishlist/',
        type: 'POST',
        data: { petId: petId },
        success: function (result) {
            console.log("Successfully hit endpoint for pet ID:", petId);
            console.log("Server response:", result);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.error("Error hitting endpoint for pet ID:", petId);
            console.error("Status:", textStatus);
            console.error("Error details:", errorThrown);
        }
    });
}
