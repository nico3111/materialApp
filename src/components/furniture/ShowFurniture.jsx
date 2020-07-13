import React from 'react';
import '../notebook/AllNotebook.css'
import AddFurniture from './AddFurniture';
import 'react-responsive-modal/styles.css';
import { Modal } from 'react-responsive-modal';

export default class ShowFurniture extends React.Component {

    state = {
        allFurniture: [],
        loading: true,
        open: false,
        showUpdate: false,
        toUpdate: null
    };

    async componentDidMount() {
        this.fetchFurniture()
    }

    fetchFurniture = async () => {
        const url = "http://192.168.0.94:8015/material/furniture";
        const response = await fetch(url);
        const data = await response.json();
        this.setState({ allFurniture: data });
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
                fetch("http://192.168.0.94:8015/material/furniture/" + id, {
                    method: 'delete',
                    mode: 'cors'
                }).then(this.fetchFurniture)

            } catch (error) {
                console.log(error)
            }
        }
    }

    async putData() {

        const body = {
            id: this.state.toUpdate.id,
            type: this.state.toUpdate.type,
            quantity: this.state.toUpdate.quantity,
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

            let result = await fetch('http://192.168.0.94:8015/material/furniture', req)
            const r = await result.text()
            if (r !== "")
                alert(r)

            this.fetchFurniture()

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
                    <div className="line-text">Mobiliar</div>
                    <div className="line2"></div>
                </div>
                <AddFurniture fetchFurniture={this.fetchFurniture} />

                {this.state.allFurniture.map(allFurniture => (
                    <div className="notebooks">
                        <div className="show-list">
                            <div className="head-text">Mobiliar</div>
                            <div>Art: {allFurniture.type}</div><br></br>
                            <div>Anzahl: {allFurniture.quantity}</div><br></br>
                            <div>Standort: {allFurniture.classroom != null ? allFurniture.classroom.addressloc.address.place : ""}</div><br></br>
                            <div>Räumlichkeit: {allFurniture.classroom != null ? allFurniture.classroom.room : ""}</div><br></br>

                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allFurniture.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allFurniture)}>Ändern</div>
                            </div>

                            {this.state.toUpdate != null && <Modal open={open} onClose={this.onCloseModal} center showCloseIcon={false}>

                                <div className="modal-wrapper">
                                    <div className="modal-main-text">Mobiliar Update
                                        <input value={this.state.toUpdate.type} name="type" onChange={(event) => this.updateWithEvent(event)}></input>
                                        <input type="number" min="1" max={Number.MAX_SAFE_INTEGER} value={this.state.toUpdate.quantity} name="quantity" onChange={(event) => this.updateWithEvent(event)}></input>
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