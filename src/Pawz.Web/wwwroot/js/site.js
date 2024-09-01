function submitAdoptionRequest() {

    var location = document.getElementById('location').value;

    if (location.trim() === '') {
        alert('Please enter your location.');
        return;
    }

    $('#adoptionRequestModal').modal('hide');
    document.getElementById('adoptionRequestForm').reset();

    alert('Adoption request submitted successfully!');
}
