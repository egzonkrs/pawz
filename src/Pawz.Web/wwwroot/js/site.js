// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function submitAdoptionRequest() {
    // Get the location value
    var location = document.getElementById('location').value;

    // Validate the input (simple validation)
    if (location.trim() === '') {
        alert('Please enter your location.');
        return;
    }

    // Here you can send the data to your server using AJAX or a form submission
    // For demonstration, we'll just log it to the console
    console.log('Adoption Request Submitted:', { location: location });

    // Close the modal
    $('#adoptionRequestModal').modal('hide');

    // Clear the form
    document.getElementById('adoptionRequestForm').reset();

    // Optionally, display a success message or take additional actions
    alert('Adoption request submitted successfully!');
}
