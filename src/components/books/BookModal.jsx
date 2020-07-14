import React from 'react';
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';
const { fetchPersons, fetchRooms } = require('../../util/HttpHelper');

export default class BookModal extends React.Component {

    state = {
        params: { id: null },
        selectedPerson: '',
        rooms: [],
        personen: [],
        paramsRoom: { id: null },
        selectedRoom: '',
        personId: '',
        toUpdate: this.props.toUpdate,
        personToUpdate: this.props.personToUpdate,
        wasPersonToUpdateUpdated: false
    }

    async componentDidMount() {
        await this.fetchRooms()
        await this.fetchPersonens()
    }

    fetchPersonens = async () => {
        const persons = await fetchPersons()
        console.log(persons)
        this.setState({ personen: persons });

    }

    fetchRooms = async () => {
        const rooms = await fetchRooms()
        this.setState({ rooms: rooms });
    }

    updateWithEvent(event) {
        const key = event.target.name;
        const value = event.target.value;

        this.setState(prev => ({
            toUpdate: {
                ...prev.toUpdate,
                [key]: value
            }
        }))

        console.log(this.state.toUpdate)
    }

    handlePersonChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedPerson = JSON.parse(value)

            this.setState({
                selectedPerson: value,
                person_id: selectedPerson.id,
            })
            console.log(selectedPerson.id)
        } else {
            this.setState({
                selectedPerson: '',
                person_id: ''
            })
        }
    }

    handlePersonToUpdateChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedPerson = JSON.parse(value)

            this.setState({
                personToUpdate: selectedPerson,
                person_id: selectedPerson.id,
                wasPersonToUpdateUpdated: true
            })
            console.log(selectedPerson.id)
        } else {
            this.setState({
                personToUpdate: '',
                person_id: ''
            })
        }
    }

    handleRoomChange = changeEvent => {
        const value = changeEvent.target.value
        if (value) {
            var selectedRoom = JSON.parse(value)

            this.setState({
                selectedRoom: value,
                location_id: selectedRoom.id,
            })
            console.log(selectedRoom)
        } else {
            this.setState({
                selectedRoom: '',
                location_id: ''
            })
        }
    }

    async putData() {

        var person_id = this.state.person_id === '' ? null : Number(this.state.person_id)
        var location_id = this.state.location_id === '' ? null : Number(this.state.location_id)


        const body = {
            id: this.state.toUpdate.id,
            title: this.state.toUpdate.title,
            isbn: this.state.toUpdate.isbn,
            quantity: this.state.toUpdate.quantity,
            person_id: person_id,
            location_id: location_id
        }

        try {
            var req = {
                method: 'put',
                mode: 'cors',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(body)
            }

            let result = await fetch('http://192.168.0.94:8015/material/book/', req)
            const r = await result.text()
            if (r !== "")
                alert(r)


            this.props.fetchBooks()

            this.props.onClose();

            console.log(body)
        } catch (error) {
            console.log(error)
        }
    }

    renderOption = (personen) => {
        if (this.state.toUpdate.person && personen.id === this.state.toUpdate.person.id) {
            return <option key={personen.id} defaultValue={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
        }

        return <option key={personen.key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
    }


    render() {
        return (
            <Modal Modal open={this.props.open} onClose={this.props.onClose}>
                <div className="modal-wrapper">
                    <div className="modal-main-text">Buch Update
                        <input value={this.state.toUpdate.title} name="title" onChange={(event) => this.updateWithEvent(event)}></input>
                        <input value={this.state.toUpdate.isbn} name="isbn" onChange={(event) => this.updateWithEvent(event)}></input>
                        <input type="number" min="1" max={Number.MAX_SAFE_INTEGER} value={this.state.toUpdate.quantity} name="quantity" onChange={(event) => this.updateWithEvent(event)}></input>
                        <select className="input-field-dropdown" value={this.state.personToUpdate} onChange={this.handlePersonToUpdateChange}>

                              {this.state.personToUpdate &&
                                    <option hidden={this.state.wasPersonToUpdateUpdated} value={JSON.stringify(this.state.personToUpdate)}>{this.state.personToUpdate.name1 + " " + this.state.personToUpdate.name2}</option>
                                }
                                <option value="">Person auswählen</option>
                              
                                {this.state.personen.map((person, key) => {
                                    return this.renderOption(person)
                                })}
                            </select>

                            <select className="input-field-dropdown" value={this.state.selectedRoom} onChange={this.handleRoomChange}>
                                <option value="" defaultValue >Raum auswählen</option>
                                {this.state.rooms.map((rooms, key) => {
                                    var x = rooms.adresslocations[0] != undefined ? " / " + rooms.adresslocations[0].address.place : ""
                                    return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + x}</option>
                                })}
                            </select>

                        <button onClick={() => this.putData()}>Ändern</button>
                    </div>
                </div>

            </Modal >
        )
    }
}