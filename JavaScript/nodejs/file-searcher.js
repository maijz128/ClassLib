/**
 * Auther: MaiJZ
 * Date: 2017/8/25
 * Github: https://github.com/maijz128
 */

const _FS = require("fs");
const _Path = require("path");

/**
 * 文件搜索器
 * 可以使用过滤器，选择你需要的文件
 * @param path  目录路径
 * @param filter function(filePath){return false; //返回true表示过滤此文件}
 * @constructor
 */
function FileSearcher(path, filter) {
    const self = this;
    self._path = null;
    self._fileList = [];
    self._filter = null;
    self._defaultFilter = function (filePath) {
        return false;   // 不过滤
    };

    self.setPath(path);
    self.setFilter(filter);
}
FileSearcher.prototype.setPath = function (path) {
    if (path)
        this._path = path;
};
FileSearcher.prototype.setFilter = function (filter) {
    if (filter)
        this._filter = filter;
};
/**
 * @param callback function(fileList){}
 */
FileSearcher.prototype.search = function (callback) {
    const self = this;
    if (self._path) {
        setTimeout(function () {
            const filter = self._filter || self._defaultFilter;
            const fileList = self._scanDir(self._path, filter);
            self._fileList = fileList;
            if (typeof (callback) === "function")
                callback(fileList)
        }, 1);

    } else {
        console.error("FileSearcher.path is null!");
        if (typeof (callback) === "function")
            callback([])
    }
};

FileSearcher.prototype.getSearchResult = function () {
    return this._fileList;
};

FileSearcher.prototype._scanDir = function (dirPath, filter) {
    var fileList = [];

    const fileNames = _FS.readdirSync(dirPath);

    for (var i = 0; i < fileNames.length; ++i) {
        const fileName = fileNames[i];
        const filePath = _Path.join(dirPath, fileName);

        const stat = _FS.statSync(filePath);
        if (stat.isDirectory()) {
            fileList = fileList.concat(scanDir(filePath, filter));

        } else {
            if (typeof (filter) === "function") {
                if (!filter(filePath))
                    fileList.push(filePath)
            } else {
                fileList.push(filePath)
            }
        }
    }

    return fileList;
}


if (typeof(exports) !== "undefined") {
    exports.FileSearcher = FileSearcher;
}