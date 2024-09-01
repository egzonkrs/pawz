$(document).ready(function () {
    console.log('DOM fully loaded and parsed');

    const $speciesSelect = $('#SpeciesId');
    const $breedSelect = $('#BreedId');

    console.log('Species select:', $speciesSelect);
    console.log('Breed select:', $breedSelect);

    if ($speciesSelect.length === 0 || $breedSelect.length === 0) {
        console.error('Species or Breed select elements not found');
        return;
    }

    $speciesSelect.on('change', function () {
        const selectedSpecies = $(this).val();
        console.log('Selected Species ID:', selectedSpecies);

        if (selectedSpecies) {
            console.log('Making AJAX request to get breeds');
            $.ajax({
                url: `/api/Pet/GetBreedsBySpecies?speciesId=${selectedSpecies}`,
                type: 'GET',
                dataType: 'json',
                success: function (breeds) {
                    console.log('Fetched Breeds:', breeds);

                    $breedSelect.empty().append('<option value="">Select Breed</option>'); // Clear previous options

                    $.each(breeds, function (index, breed) {
                        const $option = $('<option>').val(breed.value).text(breed.text);
                        $breedSelect.append($option);
                    });
                },
                error: function (xhr, status, error) {
                    console.error('Error fetching breeds:', status, error);
                }
            });
        } else {
            console.log('No species selected, clearing breed options');
            $breedSelect.empty().append('<option value="">Select Breed</option>'); // Clear options if no species selected
        }
    });
});
