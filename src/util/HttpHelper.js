function fetchData() {
    return fetch('http://localhost:5000/Material/Notebook')
    .then(response => response.json()).catch(err => console.log(err))
}

export{
    fetchData
}