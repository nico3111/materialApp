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
        toUpdate: null,
        params: { id: null },
        selectedPerson: '',
    };

    async componentDidMount() {
        this.fetchNotebooks()

        const persons = await fetchPersons()
        this.setState({ personen: persons });
    }

    fetchNotebooks = async () => {
        const url = "http://192.168.0.94:8015/material/notebook";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allnotebooks: data });
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

    async putData() {
        console.log(this.state.selectedPerson)
        console.log(this.state.selectedPerson.id)

        var person_id = this.state.person_id === '' ? null : Number(this.state.person_id)
        // var location = this.state.notebook.location_id === '' ? null : Number(this.state.notebook.location_id)


        const body = {
            id: this.state.toUpdate.id,
            serial_number: this.state.toUpdate.serial_number,
            make: this.state.toUpdate.make,
            model: this.state.toUpdate.model,
            person_id: person_id,
            // location_id: location
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

            this.fetchNotebooks()

            this.onCloseModal();

            console.log(body)
        } catch (error) {
            console.log(error)
        }
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
                {this.state.allnotebooks.map(allnotebook => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Notebook</div>
                            <div>Marke: {allnotebook.make}</div><br></br>
                            <div>Modell: {allnotebook.model}</div><br></br>
                            <div>SN: {allnotebook.serial_number}</div><br></br>
                            <div>Person: {allnotebook.person != null ? allnotebook.person.name1 + " " + allnotebook.person.name2 : ""}</div><br></br>
                            <div>Standort: {allnotebook.classroom != null ? allnotebook.classroom.addressloc.address.place : ""}</div><br></br>
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
                                        <select className="input-field-dropdown" value={this.state.selectedPerson} onChange={this.handlePersonChange}>
                                            <option value="" defaultValue>Person auswählen</option>
                                            {this.state.personen.map((personen, key) => {
                                                return <option key={key} value={JSON.stringify(personen)}>{personen.name1 + " " + personen.name2}</option>
                                            })}
                                        </select>

                                        <button onClick={() => this.putData()}>Ändern</button>
                                    </div>
                                </div>
                            </Modal>}

                        </div>
                    </div>))}

            </div>
        )
    }
}
