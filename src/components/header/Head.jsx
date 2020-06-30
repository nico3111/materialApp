import React from 'react';
import '../header/Head.css'
import logo from '../../assets/dcvLogo.png';

export default class Head extends React.Component {

    render() {
        return (
            <div className="head-wrapper">
                <div className="logo-dcv"><img src={logo} alt="DCV Logo" /></div>
                <div className="navbar">
                    <div className="links">Personen</div>
                    <div className="links">Kurse</div>
                    <div className="links">Räume</div>
                    <div className="links">Materialien</div>
                </div>
            </div>
        )
    }
}