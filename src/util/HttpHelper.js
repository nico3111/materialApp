const notebookUrl = "http://192.168.0.94:8015/material/notebook/"

function fetchPersons() {
    return fetch("http://192.168.0.94:8016/person")
    .then(response => response.json())
}

function fetchRooms() {
    return fetch("http://192.168.0.94:8019/classroom")
    .then(response => response.json())
}

function fetchNotebooks() {
    return fetch(notebookUrl)
    .then(response => response.json())
}

function deleteNotebook(id) {
    return fetch(notebookUrl + id, {
        method: 'delete',
        mode: 'cors'
    }).then(response => response.status)
}

function fetchDisplays() {
    return fetch("http://192.168.0.94:8015/material/display")
    .then(response => response.json())
}

function fetchBooks() {
    //return fetch("http://192.168.0.94:8015/material/book")
    return fetch("https://localhost:44358/material/book/")
    .then(response => response.json())
}

function fetchFurniture() {
    return fetch("http://192.168.0.94:8015/material/furniture")
    .then(response => response.json())
}

function fetchEquipment() {
    //return fetch("http://192.168.0.94:8015/material/equipment")
    return fetch("https://localhost:44358/material/equipment")
    .then(response => response.json())
} 

function fetchSearch() {
    return fetch("http://192.168.0.94:8015/material/search")
    .then(response => response.json())
}


export {
fetchPersons,
fetchRooms,
fetchNotebooks,
deleteNotebook,
fetchBooks,
fetchDisplays,
fetchFurniture,
fetchEquipment,
fetchSearch
}