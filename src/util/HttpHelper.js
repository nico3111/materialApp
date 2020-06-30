function fetchData(id) {
    return fetch('https://localhost:44323/Material/Notebook/' + id)
    .then(response => response.json()).catch(err => console.log(err))
}

export{
    fetchData
}