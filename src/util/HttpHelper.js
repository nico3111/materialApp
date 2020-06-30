function fetchData(notebook) {
    return fetch('https://localhost:44323/Material/Notebook')
    .then(response => response.json())
}

export{
    fetchData
}