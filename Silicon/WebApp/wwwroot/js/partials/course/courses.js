let form;

let categorySelector;
let categoryCurrentOption;

let pagination;

let searchField;

function selectCategory(e) {

    pagination.dataset.page = 1;
    updateSearchFilter();
}

function initializePagination() {
    pagination = document.querySelector('[class="pagination"]');
    const paginations = pagination.getElementsByClassName('btn-pagination');
    for (let i = 0; i < paginations.length; i++) {
        paginations[i].addEventListener('click', (e) => {
            const category = categoryCurrentOption.dataset.option;
            const search = searchField.value;
            console.log(e.target.dataset);
            let url = `/courses?category=${encodeURIComponent(category)}&search=${encodeURIComponent(search)}&pageNumber=${e.target.dataset.page}`;
            window.location.href = url;
        });
    }
}

document.addEventListener("DOMContentLoaded", () => {
    
    form = document.getElementById('filter');
    form.addEventListener('submit', (e) => {
        e.preventDefault();

        updateSearchFilter();
    });

    // category dropdown-related
    categorySelector = document.getElementById('Categories');
    categoryCurrentOption = categorySelector.querySelector('[class="current-option"]');
    let options = categorySelector.querySelector('[class="dropdown"]').getElementsByClassName('option');
    for (const option of options) {
        option.addEventListener('click', selectCategory);
    }

    // search field
    searchField = document.getElementById('search-field');

    // pagination
    initializePagination();
});

function updateSearchFilter() {

    try {
        const category = categoryCurrentOption.dataset.option;
        const search = searchField.value;

        let url = `/courses?category=${encodeURIComponent(category)}&search=${encodeURIComponent(search)}&pageNumber=1`;
        window.location.href = url;

        //fetch(url)
        //    .then(response => response.text())
        //    .then(data => {

        //        const parser = new DOMParser();
        //        const doc = parser.parseFromString(data, "text/html");

        //        const currentPagination = document.querySelector('[class="pagination"]');
        //        const dataPagination = doc.querySelector('[class="pagination"]');

        //        currentPagination.innerHTML = dataPagination.innerHTML;

        //        const currentCourseList = document.querySelector('[class="course-list"]');
        //        const dataCourseList = doc.querySelector('[class="course-list"]');

        //        currentCourseList.innerHTML = dataCourseList.innerHTML;

        //        initializePagination();
        //    });
    }
    catch { }

}