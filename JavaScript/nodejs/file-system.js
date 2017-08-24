/**
 * Created by MaiJZ on 2016/12/27.
 */

const _FS = require("fs");
const _Path = require("path");

const _Logger = console;

var FileSystem = {
    readDir: function (path) {
        try {
            var stats = _FS.statSync(path);
            if (stats.isDirectory()) {
                return _FS.readdirSync(path);
            }
            else {
                _Logger.log("不是文件夹！", "FileSystem.readDir(" + path + ")");
                return [];
            }
        } catch (err) {
            _Logger.error(err, "FileSystem.readDir(" + path + ")");
            return [];
        }
    },
    readJsonFile: function (path) {
        try {
            var data = _FS.readFileSync(path, "utf-8");
            return JSON.parse(data);
        }
        catch (err) {
            _Logger.log(err, "FileSystem.readJsonFile() \n" + path);
            return {};
        }
    },
    saveObjToJsonFile: function (obj, path) {
        try {
            var data = JSON.stringify(obj);
            _FS.writeFileSync(path, data);
            return true;
        }
        catch (err) {
            _Logger.log(err, "FileSystem.saveObjToFile()");
            return false;
        }
    },
    createDir: function (path) {
        _FS.mkdir(path, function (err) {
            if (err) {
                _Logger.error(err, "FileSyste.createDir()");
                return false;
            }
        });
    },
    isFile: function (path) {
        var stats = _FS.statSync(path);
        return stats.isFile();
    },
    isDirectory: function (path) {
        var stats = _FS.statSync(path);
        return stats.isDirectory();
    },
    existsSync: function (path) {
        return _FS.existsSync(path);
    },
    /**
     * @param source            /aaa/bbb/ccc/ddd.jpg
     * @param target            /aaa/bbb/
     * @returns {string|XML}    ccc/ddd.jpg
     */
    resolvePath: function (source, target) {
        var new_source = source.replace(/\\/g, '/');
        var new_target = target.replace(/\\/g, '/');
        return new_source.replace(new_target, '');
    },
    deleteFile: function (path) {
        _FS.unlink(path, function (err) {
            if (err) {
                _Logger.error(path + "\n" + err, "FileSystem.deleteFile()");
                return 0;
            }
            _Logger.log("删除文件成功。\n" + path, "FileSystem.deleteFile()");
        });
    },
    copyFile: function (scr, dest) {
        try {
            var readStream = _FS.createReadStream(scr);
            var writeStream = _FS.createWriteStream(dest);
            readStream.pipe(writeStream);
            readStream.on('error', function (err) {
                _Logger.log(scr + "\m" + dest + "\n" + err, "FileSystem.copyFile()");
            });
        } catch (err) {
            _Logger.log(scr + "\m" + dest + "\n" + err, "FileSystem.copyFile()");
        }
    },
    rename: function (oldPath, newPath) {
        try {
            _FS.renameSync(oldPath, newPath);
            return true;
        } catch (err) {
            _Logger.error(oldPath + "\n" + err, "FileSystem.rename()");
            return false;
        }
    },
    /**
     * @param path /aaa/bbb/ccc.jpg
     * @return {*} /aaa/bbb
     */
    dirName: function (path) {
        return _Path.dirname(path);
    },
    /**
     * @param path  /aaa/bbb/ccc.jpg
     * @return {*}  .jpg
     */
    extName: function (path) {
        return _Path.extname(path);
    },
    /**
     * @param path  /aaa/bbb/ccc.jpg
     * @returns {*}     ccc
     */
    baseFileName: function (path) {
        return _Path.basename(path, _Path.extname(path));
    },
    /**
     * @param path /aaa/bbb/ccc.jpg
     * @return {*}  ccc.jpg
     */
    fullFileName: function (path) {
        return _Path.basename(path);
    },
    join: function (pathA, pathB) {
        return _Path.join(pathA, pathB);
    }
};

module.exports = exports = FileSystem;