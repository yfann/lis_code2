angular.module('commonService').
    service('storage', ['$localStorage','$cookies','$cookieStore', function ($localStorage,$cookies,$cookieStore) {

        return {
            callback: null,
            setTokenAndUser: function (token, user) {
                // $localStorage.token = token;
                // $localStorage.user = user;
                //$cookies.token =  token;
                //$cookies.user = JSON.stringify(user); 
                $.cookie('token', token, { path: '/' });
                $.cookie('user', JSON.stringify(user), { path: '/' });
                localStorage.curUser = JSON.stringify(user);
                var isAdmin = user && user.emCode && user.emCode.toLowerCase() === 'admin';
                if (this.callback) {
                    this.callback(user, isAdmin);
                }
            },
            logout: function () {
                // $localStorage.token = null;
                // $localStorage.user = null;
                delete $cookies['token'];
                delete $cookies['user'];
                localStorage.removeItem('curUser');
            },
            getUser: function () {
                //return $localStorage.user;
                if (!$cookies.user) {
                    return null;
                }
                return JSON.parse($cookies.user);
            },
            isLogin: function () {
                if ($cookies.token) {
                    return true;
                } else {
                    return false;
                }
            },
            isAdmin: function (u) {
                var user = u || JSON.parse($cookies.user || '{}');
                var isAdmin = user && user.emCode && user.emCode.toLowerCase() === 'admin';
                return isAdmin;
            }
        };
    }]);