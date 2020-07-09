import React from 'react';
import '../notebook/AllNotebook.css'
import AddDisplay from './AddDisplay';
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';

export default class ShowDisplay extends React.Component {

    state = {
        allDisplays: [],
        loading: true,
        open: false,
        showUpdate: false,
        toUpdate: null
    };

    async componentDidMount() {
        this.fetchDisplays()
    }

    fetchDisplays = async () => {
        const url = "http://192.168.0.94:8015/material/display";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allDisplays: data });
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
                fetch("http://192.168.0.94:8015/material/display/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchDisplays)

            } catch (error) {
                console.log(error)
            }
        }
    }

    async putData() {

        const body = {
            id: this.state.toUpdate.id,
            model: this.state.toUpdate.model,
            make: this.state.toUpdate.make,
            serial_number: this.state.toUpdate.serial_number,
            location_id: this.state.toUpdate.location_id
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

            let result = await fetch('http://192.168.0.94:8015/material/display', req)

            this.fetchDisplays()

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
                    <div className="line-text">Bildschirme</div>
                    <div className="line2"></div>
                </div>
                <AddDisplay fetchDisplays={this.fetchDisplays} />
                {this.state.allDisplays.map(allDisplays => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Bildschirm</div>
                            <div>Marke: {allDisplays.make}</div><br></br>
                            <div>Modell: {allDisplays.model}</div><br></br>
                            <div>SN: {allDisplays.serial_number}</div><br></br>
                            <div>Standort: {allDisplays.classroom != null ? allDisplays.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allDisplays.classroom != null ? allDisplays.classroom.room : ""}</div><br></br>
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allDisplays.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allDisplays)}>Ändern</div>
                            </div>

                            {this.state.toUpdate != null && <Modal open={open} onClose={this.onCloseModal} center showCloseIcon={false}>

                                <div className="modal-wrapper">
                                    <div className="modal-main-text">Display Update
                                        <input value={this.state.toUpdate.serial_number} name="serial_number" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input value={this.state.toUpdate.model} name="model" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input value={this.state.toUpdate.make} name="make" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input value={this.state.toUpdate.location_id} name="location_id" onChange={(event) => this.updateWithEvent(event)}></input>
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