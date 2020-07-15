import React from 'react';
import '../notebook/AllNotebook.css'
import AddFurniture from './AddFurniture';
import FurnitureModal from '../furniture/FurnitureModal';
const { fetchFurniture } = require('../../util/HttpHelper')

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
        const furniture = await fetchFurniture()
        this.setState({ allFurniture: furniture });
        console.log(furniture)
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
                            <div className="main-text">
                                <div className="head-text">Mobiliar</div>
                                <div>Art: {allFurniture.type}</div><br></br>
                                <div>Anzahl: {allFurniture.quantity}</div><br></br>
                                {console.log(allFurniture)}
                                {console.log("loc " + allFurniture.classroom.addressloc)}
                                {console.log("add" + allFurniture.classroom.addressloc.address)}
                                {console.log("place " + allFurniture.classroom.addressloc.address.place)}
                                <div>Standort: {allFurniture.classroom && allFurniture.classroom.addressloc != null ? allFurniture.classroom.addressloc.address.place : ""}</div><br></br>
                                {console.log(allFurniture)}

                                <div>Räumlichkeit: {allFurniture.classroom != null ? allFurniture.classroom.room : ""}</div><br></br>
                            </div>
                            <div className="button-wrapper">
                                <div className="add-button2" onClick={() => this.deleteData(allFurniture.id)}>Löschen</div>
                                <div className="add-button2" onClick={() => this.onOpenModal(allFurniture)}>Ändern</div>
                            </div>

                            {this.state.toUpdate != null && <FurnitureModal fetchFurniture={this.fetchFurniture} toUpdate={this.state.toUpdate} open={open} onClose={this.onCloseModal} center showCloseIcon={false}>
                            </FurnitureModal>}

                        </div>
                    </div>))}
            </div>
        )
    }
}