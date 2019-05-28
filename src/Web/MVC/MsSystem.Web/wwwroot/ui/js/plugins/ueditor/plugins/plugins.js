//新建表单
UE.plugins['formadd'] = function ()
{
    var me = this, thePlugins = 'formadd';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/attribute.aspx?new=1',
                name: thePlugins,
                editor: this,
                title: '新建表单',
                cssRules: "width:400px;height:310px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
};
//表单属性
UE.plugins['formattribute'] = function ()
{
    var me = this, thePlugins = 'formattribute';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/attribute.aspx',
                name: thePlugins,
                editor: this,
                title: '设置表单属性',
                cssRules: "width:400px;height:310px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
};
//打开表单
UE.plugins['formopen'] = function ()
{
    var me = this, thePlugins = 'formopen';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var timestemp = new Date().valueOf();
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/open.aspx',
                name: thePlugins + '_' + timestemp,
                editor: this,
                title: '打开表单',
                cssRules: "width:850px;height:400px;"
            });
            dialog.render();
            dialog.open();
        }
    };
};
//保存表单
UE.plugins['formsave'] = function ()
{
    var me = this, thePlugins = 'formsave';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/save.aspx',
                name: thePlugins,
                editor: this,
                title: '保存表单',
                cssRules: "width:300px;height:120px;"
            });
            dialog.render();
            dialog.open();
        }
    };
};
//表单另存为
UE.plugins['formsaveas'] = function ()
{
    var me = this, thePlugins = 'formsaveas';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/saveas.aspx',
                name: thePlugins,
                editor: this,
                title: '表单另存为',
                cssRules: "width:400px;height:180px;"
            });
            dialog.render();
            dialog.open();
        }
    };
};
//发布表单
UE.plugins['formcompile'] = function ()
{
    var me = this, thePlugins = 'formcompile';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/publish.aspx',
                name: thePlugins,
                editor: this,
                title: '发布表单',
                cssRules: "width:300px;height:120px;"
            });
            dialog.render();
            dialog.open();
        }
    };
};
//文本框
UE.plugins['formtext'] = function ()
{
    var me = this, thePlugins = 'formtext';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/text.aspx',
                name: thePlugins,
                editor: this,
                title: '文本框',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>文本框: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//文本域
UE.plugins['formtextarea'] = function ()
{
    var me = this, thePlugins = 'formtextarea';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/textarea.aspx',
                name: thePlugins,
                editor: this,
                title: '文本域',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>文本域: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//html编辑器
UE.plugins['formhtml'] = function ()
{
    var me = this, thePlugins = 'formhtml';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/html.aspx',
                name: thePlugins,
                editor: this,
                title: 'HTML编辑器',
                cssRules: "width:500px;height:260px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>HTML编辑器: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//单选按钮组
UE.plugins['formradio'] = function ()
{
    var me = this, thePlugins = 'formradio';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/radio.aspx',
                name: thePlugins,
                editor: this,
                title: '单选按钮组',
                cssRules: "width:600px;height:360px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>单选按钮组: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//复选按钮组
UE.plugins['formcheckbox'] = function ()
{
    var me = this, thePlugins = 'formcheckbox';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/checkbox.aspx',
                name: thePlugins,
                editor: this,
                title: '复选按钮组',
                cssRules: "width:600px;height:360px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>复选按钮组: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//组织机构选择框
UE.plugins['formorg'] = function ()
{
    var me = this, thePlugins = 'formorg';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/org.aspx',
                name: thePlugins,
                editor: this,
                title: '组织机构选择框',
                cssRules: "width:500px;height:280px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>组织机构选择框: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//数据字典选择框
UE.plugins['formdictionary'] = function ()
{
    var me = this, thePlugins = 'formdictionary';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/dictionary.aspx',
                name: thePlugins,
                editor: this,
                title: '数据字典选择框',
                cssRules: "width:500px;height:280px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_dict")
        {
            var html = popup.formatHtml('<nobr>数据字典选择框: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//日期时间选择
UE.plugins['formdatetime'] = function ()
{
    var me = this, thePlugins = 'formdatetime';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/datetime.aspx',
                name: thePlugins,
                editor: this,
                title: '日期时间选择',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>日期时间选择: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//隐藏域
UE.plugins['formhidden'] = function ()
{
    var me = this, thePlugins = 'formhidden';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/hidden.aspx',
                name: thePlugins,
                editor: this,
                title: '隐藏域',
                cssRules: "width:500px;height:280px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>隐藏域: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//下拉列表框
UE.plugins['formselect'] = function ()
{
    var me = this, thePlugins = 'formselect';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/select.aspx',
                name: thePlugins,
                editor: this,
                title: '下拉列表框',
                cssRules: "width:600px;height:360px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>下拉列表框: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//附件上传
UE.plugins['formfiles'] = function ()
{
    var me = this, thePlugins = 'formfiles';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/files.aspx',
                name: thePlugins,
                editor: this,
                title: '附件上传',
                cssRules: "width:500px;height:280px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>附件上传: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//子表
UE.plugins['formsubtable'] = function ()
{
    var me = this, thePlugins = 'formsubtable';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/subtable.aspx',
                name: thePlugins,
                editor: this,
                title: '子表',
                cssRules: "width:700px;height:460px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>子表: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//Label标签
UE.plugins['formlabel'] = function ()
{
    var me = this, thePlugins = 'formlabel';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/label.aspx',
                name: thePlugins,
                editor: this,
                title: 'Label标签',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>Label标签: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//按钮
UE.plugins['formbutton'] = function ()
{
    var me = this, thePlugins = 'formbutton';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/button.aspx',
                name: thePlugins,
                editor: this,
                title: '按钮',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>按钮: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};
//grid数据表格
UE.plugins['formgrid'] = function ()
{
    var me = this, thePlugins = 'formgrid';
    me.commands[thePlugins] = {
        execCommand: function ()
        {
            var dialog = new UE.ui.Dialog({
                iframeUrl: this.options.UEDITOR_HOME_URL + 'plugins/dialogs/grid.aspx',
                name: thePlugins,
                editor: this,
                title: '数据表格',
                cssRules: "width:600px;height:300px;",
                buttons: [
				{
				    className: 'edui-okbutton',
				    label: '确定',
				    onclick: function ()
				    {
				        dialog.close(true);
				    }
				},
				{
				    className: 'edui-cancelbutton',
				    label: '取消',
				    onclick: function ()
				    {
				        dialog.close(false);
				    }
				}]
            });
            dialog.render();
            dialog.open();
        }
    };
    var popup = new baidu.editor.ui.Popup({
        editor: this,
        content: '',
        className: 'edui-bubble',
        _edittext: function ()
        {
            baidu.editor.plugins[thePlugins].editdom = popup.anchorEl;
            me.execCommand(thePlugins);
            this.hide();
        },
        _delete: function ()
        {
            if (window.confirm('确认删除该控件吗？'))
            {
                baidu.editor.dom.domUtils.remove(this.anchorEl, false);
            }
            this.hide();
        }
    });
    popup.render();
    me.addListener('mouseover', function (t, evt)
    {
        evt = evt || window.event;
        var el = evt.target || evt.srcElement;
        var type1 = el.getAttribute('type1');
        if (/input/ig.test(el.tagName) && type1 == "flow_" + thePlugins.replace('form', ''))
        {
            var html = popup.formatHtml('<nobr>数据表格: <span onclick=$$._edittext() class="edui-clickable">编辑</span>&nbsp;&nbsp;<span onclick=$$._delete() class="edui-clickable">删除</span></nobr>');
            if (html)
            {
                popup.getDom('content').innerHTML = html;
                popup.anchorEl = el;
                popup.showAnchor(popup.anchorEl);
            } else
            {
                popup.hide();
            }
        }
    });
};