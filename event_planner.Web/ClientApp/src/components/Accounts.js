import React from 'react';
import axios from 'axios';

const AccountListing = (props) => {
    return (
        <tr>
            <td>{props.account.accountId}</td>
            <td>{props.account.email}</td>
            <td>{props.account.password}</td>
            <td>{props.account.createdDate}</td>
            <td>{props.account.modifiedDate}</td>
            <td><button onClick={() => props.onEditClick(props.account)}>Edit</button></td>
            <td><button onClick={() => props.onDeleteClick(props.account.accountId)}>Delete</button></td>
        </tr>
    );
}

const BASE_URL = "api/Accounts";
class Accounts extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            id: 0,
            email: "",
            password: "",
            accounts: []
        }
    }

    componentDidMount = () => {
        this.getAccounts();
    }

    getAccounts = () => {
        axios.get(BASE_URL)
            .then( resp => {
                let accounts = resp.data;
                console.log(accounts);
                this.setState({
                    accounts: accounts
                })
            });
    }

    createAccount = () => {
        if(this.state.id !== 0) {
            this.editAccount();
            return;
        }
        var model = {
            email: this.state.email,
            password: this.state.password
        }
        console.log(model);
        axios.post(BASE_URL, model)
            .then(resp => {
                this.getAccounts();
            })
    }

    editAccount = () => {
        var model = {
            accountId: this.state.id,
            email: this.state.email,
            password: this.state.password
        }
        console.log("Editing Account, ", model);
        axios.put(`${BASE_URL}/${model.accountId}`, model)
            .then(resp => {
                this.getAccounts();
            })
    }

    inputChanged = (e) => {
        let name = e.target.name;
        let value = e.target.value;
        this.setState({
            [name]: value
        })
    }

    cancelForm = () => {
        this.setState({
            id: 0,
            email: "",
            password: ""
        })
    }

    handleEditClick = (account) => {
        console.log(account);
        this.setState({
            id: account.accountId,
            email: account.email,
            password: account.password
        })
    }

    handleDeleteClick = (id) => {
        console.log(id);
        axios.delete(BASE_URL + `/${id}`)
            .then(resp => {
                let accList = this.state.accounts;
                accList = accList.filter(a => a.accountId !== id);
                this.setState({
                    accounts: accList
                })
            })
    }

    render() {
        return (
            <div>
                <div>
                    <input name="email" value={this.state.email} onChange={this.inputChanged} type="text" />
                    <input name="password" value={this.state.password} onChange={this.inputChanged} type="text" />
                    <button type="button" onClick={this.createAccount}>Create Account</button>
                    <button type="button" onClick={this.cancelForm}>Cancel</button>
                </div>
                <table className="table">
                    <thead>
                        <tr>
                            <th>Account Id</th>
                            <th>Email</th>
                            <th>Password</th>
                            <th>Created Date</th>
                            <th>Modified Date</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state.accounts.map(a => 
                            <AccountListing key={a.accountId} account={a} onEditClick={this.handleEditClick} onDeleteClick={this.handleDeleteClick}/>
                        )}
                    </tbody>
                </table>
            </div>
        );
    }
}

export default Accounts;