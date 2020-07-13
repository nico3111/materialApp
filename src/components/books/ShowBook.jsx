import React from 'react';
import '../notebook/AllNotebook.css'
import AddBook from './AddBook';
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';
import BookModal from './BookModal';
const { fetchPersons } = require('../../util/HttpHelper')

export default class ShowBook extends React.Component {

    state = {
        allBooks: [],
        loading: true,
        open: false,
        showUpdate: false,
        toUpdate: null,
        params: { id: null },
        selectedPerson: ''
    };

    async componentDidMount() {
        this.fetchBooks()

        // const persons = await fetchPersons()
        // this.setState({ personen: persons });
    }

    fetchBooks = async () => {
        const url = "http://192.168.0.94:8015/material/book";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allBooks: data });
    }

    // updateWithEvent(event) {
    //     const key = event.target.name;
    //     const value = event.target.value;

    //     this.setState(prev => ({
    //         toUpdate: {
    //             ...prev.toUpdate,
    //             [key]: value
    //         }
    //     }))

    //     console.log(this.state.toUpdate)
    // }

    onOpenModal = (toUpdate) => {
        this.setState({
            open: true,
            toUpdate: toUpdate
        });
    }
    onCloseModal = () => {
        this.setState({
            open: false,
            toUpdate: null
        });
    }

    async deleteData(id) {

        if (window.confirm("Möchten Sie wirklich löschen")) {

            try {
                fetch("http://192.168.0.94:8015/material/book/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchBooks)

            } catch (error) {
                console.log(error)
            }
        }
    }

    // handlePersonChange = changeEvent => {
    //     var selectedPerson = JSON.parse(changeEvent.target.value);

    //     this.setState({
    //         selectedPerson: changeEvent.target.value,
    //         id: selectedPerson.id,
    //     })
    // }

    // async putData() {
    //     console.log(this.state.selectedPerson)
    //     console.log(this.state.id)

    //     var person_id = this.state.id === '' ? null : Number(this.state.id)
    //     // var location = this.state.notebook.location_id === '' ? null : Number(this.state.notebook.location_id)


    //     const body = {
    //         id: this.state.toUpdate.id,
    //         title: this.state.toUpdate.title,
    //         isbn: this.state.toUpdate.isbn,
    //         quantity: this.state.toUpdate.quantity,
    //         person_id: person_id,
    //         //     location_id: location
    //     }

    //     try {
    //         var req = {
    //             method: 'put',
    //             mode: 'cors',
    //             headers: {
    //                 'Content-Type': 'application/json',
    //             },
    //             body: JSON.stringify(body)
    //         }

    //         let result = await fetch('http://192.168.0.94:8015/material/book/', req)
    //         const r = await result.text()
    //         if (r !== "")
    //             alert(r)

    //         this.fetchBooks()

    //         this.onCloseModal();

    //         console.log(body)
    //     } catch (error) {
    //         console.log(error)
    //     }
    // }


    render() {
        if (this.props.isShowing === false) {
            return null;
        }

        const { open } = this.state;

        return (
            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div>
                    <div className="line-text">Bücher</div>
                    <div className="line2"></div>
                </div>
                <AddBook fetchBooks={this.fetchBooks} />
                {this.state.allBooks.map(allBooks => {

                    return (
                        <div className="notebooks">
                            <div className="show-list">
                                <div className="head-text">Buch</div>
                                <div>Titel: {allBooks.title}</div><br></br>
                                <div>ISBN: {allBooks.isbn}</div><br></br>
                                <div>Anzahl: {allBooks.quantity}</div><br></br>
                                <div>Person: {allBooks.person != null ? allBooks.person.name1 + " " + allBooks.person.name2 : ""}</div><br></br>
                                <div>Standort: {allBooks.classroom && allBooks.classroom.addressloc != null ? allBooks.classroom.addressloc.address.place : ""}</div><br></br>
                                <div>Räumlichkeit: {allBooks.classroom != null ? allBooks.classroom.room : ""}</div><br></br>

                                <div className="button-wrapper">
                                    <div className="add-button2" onClick={() => this.deleteData(allBooks.id)}>Löschen</div>
                                    <div className="add-button2" onClick={() => this.onOpenModal(allBooks)}>Ändern</div>
                                </div>

                                {this.state.toUpdate != null && <BookModal fetchBooks={this.fetchBooks} toUpdate={this.state.toUpdate} personToUpdate={this.state.toUpdate.person} open={open} onClose={this.onCloseModal} center showCloseIcon={false}>
                                </BookModal>}

                            </div>
                        </div>
                    )
                }
                )}
            </div>
        )
    }
}