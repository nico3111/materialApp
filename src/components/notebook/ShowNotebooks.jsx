import React from 'react';
import '../notebook/AllNotebook.css'
import AddNotebook from './AddNotebook';
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';
const { fetchPersons } = require('../../util/HttpHelper')

export default class ShowNotebook extends React.Component {

    state = {
        loading: true,
        allnotebooks: [],
        open: false,
        showUpdate: false,
        params: { id: null },
        selectedPerson: '',
        rooms: [],
        paramsRoom: { id: null },
        selectedRoom: '',
        personId: '',
        
        toUpdate: null,
        personToUpdate: null,
        wasPersonToUpdateUpdated: false
    };

    async componentDidMount() {
        this.fetchNotebooks()
        this.fetchRooms()

        const persons = await fetchPersons()
        this.setState({ personen: persons });
    }

    fetchNotebooks = async () => {
        const url = "http://192.168.0.94:8015/material/notebook";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allnotebooks: data });
    }

    fetchRooms = async () => {
        const url = "http://192.168.0.94:8019/classroom";
        const response = await fetch(url);
        const data = await response.json();
        console.log(data)
        this.setState({ rooms: data });
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

    onOpenModal = (toUpdate) => {
        this.setState({
            open: true,
            toUpdate: toUpdate,
            personToUpdate: toUpdate.person
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
                fetch("http://192.168.0.94:8015/material/notebook/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchNotebooks)

            } catch (error) {
                console.log(error)
            }
        }
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

        var person_id = this.state.person_id === '' ? null : Number(this.state.personToUpdate.id)
        var location_id = this.state.location_id === '' ? null : Number(this.state.location_id)


        const body = {
            id: this.state.toUpdate.id,
            serial_number: this.state.toUpdate.serial_number,
            make: this.state.toUpdate.make,
            model: this.state.toUpdate.model,
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

            let result = await fetch('http://192.168.0.94:8015/material/notebook/', req)
            const r = await result.text()
            if (r !== "")
                alert(r)


            this.fetchNotebooks()

            this.onCloseModal();

            console.log(body)
        } catch (error) {
            console.log(error)
        }
    }

    renderOption = (personen) => {
        if (this.state.toUpdate.person && personen.id === this.state.personToUpdate.id) {
            return <option key={personen.id} defaultValue={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option> 
        } 
        
        return <option key={personen.key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
    }

    render() {

        if (this.props.isShowing === false) {
            return null;
        }
        const { open } = this.state;

        return (

            <div className="notebooks-wrapper">

                <div className="line-wrapper">
                    <div className="line"></div>
                    <div className="line-text">Notebooks</div>
                    <div className="line2"></div>
                </div>
                <AddNotebook fetchNotebooks={this.fetchNotebooks} />
                {this.state.allnotebooks.map(allnotebook => {

                    return (
                        <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Notebook</div>
                            <div>Marke: {allnotebook.make}</div><br></br>
                            <div>Modell: {allnotebook.model}</div><br></br>
                            <div>SN: {allnotebook.serial_number}</div><br></br>
                            <div>Person: {allnotebook.person != null ? allnotebook.person.name1 + " " + allnotebook.person.name2 : ""}</div><br></br>
                            <div>Standort: {allnotebook.classroom && allnotebook.classroom.addressloc != null ? allnotebook.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allnotebook.classroom != null ? allnotebook.classroom.room : ""}</div><br></br>

                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allnotebook.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allnotebook)}>Ändern</div>
                            </div>


                            {this.state.toUpdate != null && <Modal open={open} onClose={this.onCloseModal} center showCloseIcon={false}>

                                <div className="modal-wrapper">
                                    <div className="modal-main-text">Update Notebook
                                        <input value={this.state.toUpdate.make} name="make" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input value={this.state.toUpdate.model} name="model" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input value={this.state.toUpdate.serial_number} name="serial_number" onChange={(event) => this.updateWithEvent(event)}></input>
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
                                                return <option key={key} value={JSON.stringify(rooms)}>{rooms.room + " / " + rooms.adresslocations[0].address.place}</option>
                                            })}
                                        </select>

                                        <button onClick={() => this.putData()}>Ändern</button>
                                    </div>
                                </div>
                            </Modal>}

                        </div>
                    </div>
                    )
                }
                )}
            </div>
        )
    }
}
